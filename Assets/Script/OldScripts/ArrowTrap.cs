using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform[] spawnPoints;
    public float fireRate = 2f;  // kaç saniyede bir ateþ edecek
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            FireArrow();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireArrow()
    {
        if (spawnPoints.Length == 0 || arrowPrefab == null)
            return;

        // Rastgele bir çýkýþ noktasý seç
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Oku oluþtur
        Instantiate(arrowPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
