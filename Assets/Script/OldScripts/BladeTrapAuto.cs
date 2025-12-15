using UnityEngine;

public class BladeTrapAuto : MonoBehaviour
{
    public float swingSpeed = 2f;      // Salýným hýzý
    public float swingAngle = 45f;     // Maksimum açý
    public float startDelay = 0f;      // Gecikme süresi
    public float damage = 40f;
    public AudioSource audioSource;
    public AudioClip bladeSound;

    private float timer = 0f;
    private bool started = false;
    private float localTime = 0f;      // Her býçak için ayrý zaman

    private Quaternion initialRotation; // Baþlangýç rotasyonu kaydet

    void Start()
    {
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // Baþlama gecikmesi
        if (!started)
        {
            timer += Time.deltaTime;
            if (timer >= startDelay)
            {
                audioSource.PlayOneShot(bladeSound);
                started = true;
                timer = 0f;
            }
            else return;
        }

        // Her býçak kendi zamanýnda salýnýr
        localTime += Time.deltaTime;
        float angle = Mathf.Sin(localTime * swingSpeed) * swingAngle;

        // Baþlangýç rotasyonuna göre salýným
        transform.localRotation = initialRotation * Quaternion.Euler(angle, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                player.TakeDamage(damage);
        }
    }
}
