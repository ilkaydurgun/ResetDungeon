using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public GameObject[] playerPrefabs;
    public Transform respawnPoint;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject toBeContinuedPanel;
    public GameObject playerCamera;
    public GameObject gameUI;

    [Header("In-Game UI")]
  
    public TextMeshProUGUI deathCountText;  // Sağ üstte

    private GameObject currentPlayer;
    private int currentIndex = 0;
    private bool gameStarted = false;
    private int deathCount = 0;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        toBeContinuedPanel.SetActive(false);
        gameUI.SetActive(false);
        // UI'yi başlat
        UpdateUI();
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        playerCamera.SetActive(false);
        gameUI.SetActive(true);
        gameStarted = true;

        deathCount = 0;
        UpdateUI();

        SpawnPlayer();
        Time.timeScale = 1f;
    }

    public void SpawnPlayer()
    {
        if (respawnPoint == null || playerPrefabs.Length == 0) return;

        if (currentPlayer != null)
        {
            
        }

        GameObject prefabToSpawn = playerPrefabs[currentIndex];
        currentPlayer = Instantiate(prefabToSpawn, respawnPoint.position, respawnPoint.rotation);

        // PlayerController bağlantısı
        PlayerController pc = currentPlayer.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.gameManager = this;

            // Oyuncu canını UI'da göster
           
        }

        // Karakter sırasını döngüsel hale getir
        currentIndex++;
        if (currentIndex >= playerPrefabs.Length)
            currentIndex = 0;
    }

    public void OnPlayerDeath()
    {
        if (!gameStarted) return;

        deathCount++;
        UpdateUI();

        // 3 saniye sonra yeniden doğur
        Invoke(nameof(SpawnPlayer), 3f);
    }

    public void ShowToBeContinued()
    {
        toBeContinuedPanel.SetActive(true);
        Time.timeScale = 0f;
        Invoke(nameof(ReturnToMainMenu), 5f);
    }

    public void ReturnToMainMenu()
    {
        toBeContinuedPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        Time.timeScale = 1f;
        gameStarted = false;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan çıkılıyor...");
    }

    // 👇 Can UI'sini güncelle
  

    // 👇 Ölüm sayısı UI'sini güncelle
    private void UpdateUI()
    {
        if (deathCountText != null)
            deathCountText.text = "Ölüm: " + deathCount;
    }
}
