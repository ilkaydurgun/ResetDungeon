using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    private float currentSpeed;


    [Header("Mouse Settings")]
    public float mouseSensivity = 200f;
    private float rotationY = 0f;


    private Rigidbody rb;
    private Animator animator;
    private Camera playerCamera;

    void Start()
    {
        // Get Components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        //Call Methods
        HandleMovement();
        HandleMouseLook();
        HandleAttack();
    }


    void HandleMovement()
    {

       
        // Take horizontal and vertical input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // Running State
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;
        // Calculate movement direction
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        Vector3 moveDir = move.normalized * currentSpeed;

        Vector3 newVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
        rb.linearVelocity = newVelocity;

        // Update Animator Parameters
        animator.SetBool("isWalking", vertical > 0);
        animator.SetBool("isWalkingBackward", vertical < 0);
        animator.SetBool("isWalkingLeft", horizontal < 0);
        animator.SetBool("isWalkingRight", horizontal > 0);
        animator.SetBool("isRunning", isRunning && vertical > 0);
    }
    void HandleMouseLook()
    {
        //Take mouse X input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        // Move mouse right
        if (mouseX > 0.1f)
        {
            animator.SetBool("LookRight", true);
            animator.SetBool("LookLeft", false);
        }
        // Move mouse left
        else if (mouseX < -0.1f)
        {
            animator.SetBool("LookRight", false);
            animator.SetBool("LookLeft", true);
        }
        // Mouse idle
        else
        {
            animator.SetBool("LookRight", false);
            animator.SetBool("LookLeft", false);
        }
    }


    void HandleAttack()
    {
       // Blocking State
        bool isBlocking = Input.GetMouseButton(1);
        animator.SetBool("Block",isBlocking);
        if (Input.GetMouseButtonDown(0))
        {
            // Attack
            animator.SetTrigger("Attack");
        }
       
    }
}
