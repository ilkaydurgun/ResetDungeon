using UnityEngine;

public class LogSwing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Ýlk sallanma kuvveti")]
    public float initialForce = 300f;

    private Rigidbody rb;
    private bool started = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
       
    }

    public void StartSwing()
    {
        if (started) return;
        started = true;

        // Kütüðe ilk yönlü kuvvet verelim (örneðin saða doðru)
        rb.AddForce(transform.right * initialForce);
        Debug.Log("Kütük sallanmaya baþladý!");
    }
}
