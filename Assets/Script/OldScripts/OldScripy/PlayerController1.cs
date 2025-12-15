using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    private float currentSpeed;

    [Header("Mouse Settings")]
    public float mouseSensivity = 200f;

    [Header("Health")]
    public float maxhealth = 100;
    private float currentHealth;
    private bool isDead = false;

    private Rigidbody rb;
    private Animator animator;
    private Camera playerCamera;

    private float rotationY = 0f;
    private bool isGrounded = true;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        currentHealth = maxhealth;

     
    }

    void Update()
    {
        if (isDead) return;

        HandleMouseLook();
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    void HandleMovement()
    {
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        Vector3 moveDir = move.normalized * currentSpeed;

        Vector3 newVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
        rb.linearVelocity = newVelocity;

    
        animator.SetBool("isWalkingForward", vertical > 0);
        animator.SetBool("isWalkingBackward", vertical < 0);
        animator.SetBool("isWalkingLeft", horizontal < 0);
        animator.SetBool("isWalkingRight", horizontal > 0);
        animator.SetBool("isRunning", isRunning && vertical > 0);
        animator.SetBool("isGrounded", isGrounded);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Debug.Log("Jump!");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        animator.SetTrigger("TakeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        audioSource.PlayOneShot(deathSound);
    
        animator.SetBool("isWalkingForward", false);
        animator.SetBool("isWalkingBackward", false);
        animator.SetBool("isWalkingLeft", false);
        animator.SetBool("isWalkingRight", false);
        animator.SetBool("isRunning", false);

        
        animator.SetBool("isDead", true);
        animator.SetTrigger("Die");

        
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        GetComponent<Collider>().enabled = false;

        if (gameManager != null)
            gameManager.OnPlayerDeath();
    }
}
