using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerUI : MonoBehaviour
{
    public GameObject toBeContinuedPanel; // Paneli Inspector'dan ata
    public GameObject mainMenuPanel;      // Ana menü paneli
    public float delay = 5f;              // UI sonrasý gecikme

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player")) // Player tag'i kontrol et
        {
            triggered = true;
            StartCoroutine(ShowUI());
        }
    }

    private System.Collections.IEnumerator ShowUI()
    {
        // Oyunu durdur
        Time.timeScale = 0f;

        // "To Be Continued" panelini aç
        toBeContinuedPanel.SetActive(true);

        // gerçek zamanlý bekle
        float pauseEndTime = Time.realtimeSinceStartup + delay;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return null;
        }

        // Paneli kapat
        toBeContinuedPanel.SetActive(false);

        // Ana menüyü aç
        mainMenuPanel.SetActive(true);

        // Oyunu tekrar baþlat
        Time.timeScale = 1f;
    }
}
