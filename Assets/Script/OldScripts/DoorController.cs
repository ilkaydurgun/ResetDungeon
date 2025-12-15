using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Transform door;          // Kapý modeli
    public float openHeight = 3f;   // Kapý yukarý ne kadar kalksýn
    public float openSpeed = 2f;    // Açýlma hýzý
    public float closeSpeed = 1f;
    public bool isOpen = false;     // Kapý açýk mý?
    public AudioSource audioSource;
    public AudioClip openingsound;
    private Vector3 closedPos;      // Kapýnýn kapalý pozisyonu
    private Vector3 openPos;        // Kapýnýn açýk pozisyonu

    void Start()
    {
        // Kapýnýn baþlangýç pozisyonunu kaydet
        closedPos = door.localPosition;
        openPos = closedPos + new Vector3(0, openHeight, 0);
    }

    void Update()
    {
        // Hedef pozisyona doðru yumuþak geçiþ
        if (isOpen)
            door.localPosition = Vector3.Lerp(door.localPosition, openPos, Time.deltaTime * openSpeed);
        else
            door.localPosition = Vector3.Lerp(door.localPosition, closedPos, Time.deltaTime * openSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(openingsound);
            isOpen = true;
            Debug.Log("Kapý yukarý kalkýyor!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            Debug.Log("Kapý kapanýyor!");
        }
    }
}
