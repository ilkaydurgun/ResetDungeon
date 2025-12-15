using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public Rigidbody logRigidbody;
    public float delayBeforeFall = 0.5f;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected: " + other.name); // TEST SATIRI
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;
            Invoke(nameof(ReleaseLog), delayBeforeFall);
        }
    }

    void ReleaseLog()
    {
        logRigidbody.isKinematic = false;
    }
}
