using UnityEngine;

public class SawSpin : MonoBehaviour
{
    public float spinSpeed = 400.0f;
    public float minZ = -0.5f;
    public float maxZ = 2.5f;
    public float speed = 2f;
    private int direction = 1;
    public float damage = 100f; // Ölüm hasarý
    public AudioSource audioSource;
    public AudioClip sawSound;

    private void Start()
    {
        
        audioSource.PlayOneShot(sawSound);

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, spinSpeed) * Time.deltaTime);
        transform.position += Vector3.forward * speed * Time.deltaTime * direction;

        // Sýnýr kontrolü
        if (transform.position.z >= maxZ)
        {
            direction = -1;
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }
        else if (transform.position.z <= minZ)
        {
            direction = 1;
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player’a temas ettiyse
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
