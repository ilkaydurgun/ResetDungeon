using UnityEngine;
using UnityEngine.Audio;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;
    public float damage = 25f;
    public AudioSource audioSource;
    public AudioClip hitSound;

    void Start()
    {
        audioSource.PlayOneShot(hitSound);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
