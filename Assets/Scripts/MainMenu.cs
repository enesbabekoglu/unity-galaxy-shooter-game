using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Oyunu başlatma fonksiyonu
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // eski adı: "SampleScene"
    }
}