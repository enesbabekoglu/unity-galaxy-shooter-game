using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class UIManager_sc : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Puanı gösterecek Text (TMP) objesi
    private int score = 0; // Oyuncunun puanı

    public Image livesImage; // Can görselini gösterecek UI bileşeni
    public Sprite threeLivesSprite; // 3 can için görsel
    public Sprite twoLivesSprite; // 2 can için görsel
    public Sprite oneLifeSprite; // 1 can için görsel
    public Sprite noLivesSprite; // 0 can (Game Over) için görsel

    public TextMeshProUGUI gameOverText; // Game Over yazısı
    public TextMeshProUGUI gameOverRestartText; // "Press R to restart" yazısı
    private bool isGameOver = false; // Oyun bitti mi kontrolü

    void Start()
    {
        UpdateScore(0); // Başlangıçta skoru sıfırla
        HideGameOver(); // Game Over yazısını gizle
    }

    void Update()
    {
        // R tuşuna basıldığında oyunu yeniden başlat
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount; // Puanı arttır
        scoreText.text = "Score: " + score; // UI'da güncelle
    }

    public void UpdateLivesUI(int lives)
    {
        // Mevcut cana göre doğru görseli ayarla
        if (lives == 3)
        {
            livesImage.sprite = threeLivesSprite;
        }
        else if (lives == 2)
        {
            livesImage.sprite = twoLivesSprite;
        }
        else if (lives == 1)
        {
            livesImage.sprite = oneLifeSprite;
        }
        else if (lives <= 0)
        {
            livesImage.sprite = noLivesSprite;
            ShowGameOver(); // Game Over ekranını göster
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true); // Game Over yazısını aktif et
        gameOverRestartText.gameObject.SetActive(true); // Restart yazısını aktif et
        Time.timeScale = 0f; // Oyun durdurulur
        StartCoroutine(FlickerGameOverText()); // Flicker efekti başlat
    }

    public void HideGameOver()
    {
        gameOverText.gameObject.SetActive(false); // Game Over yazısını gizle
        gameOverRestartText.gameObject.SetActive(false); // Restart yazısını gizle
    }

    private IEnumerator FlickerGameOverText()
    {
        while (true)
        {
            gameOverText.gameObject.SetActive(!gameOverText.gameObject.activeSelf); // Yazıyı gizle/göster
            yield return new WaitForSecondsRealtime(0.5f); // Gerçek zaman kullanımı
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyun hızını tekrar normal hale getir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
