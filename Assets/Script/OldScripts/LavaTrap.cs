using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    public float damage = 100f; // Ölüm hasarý

    private void OnTriggerEnter(Collider other)
    {
        // Player'a temas ettiyse
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
