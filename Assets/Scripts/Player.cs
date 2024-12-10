using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Hareket hızı
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;

    public Transform laserSpawnPoint; // Lazerin çıkış noktası
    public GameObject laserPrefab;    // Normal lazer prefab'i
    public float laserSpeed = 10f;    // Lazerin hareket hızı

    private bool isTripleShotActive = false; // Üçlü atış durumu
    private float tripleShotDuration = 10f; // Üçlü atış süresi

    private bool isSpeedBoostActive = false; // Hız artışı durumu
    private float speedBoostDuration = 5f; // Hız artışı süresi
    private float defaultSpeed; // Varsayılan hız

    private int lives = 3; // Oyuncunun başlangıç canı
    private UIManager_sc uiManager; // UIManager'a referans

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSpeed = speed; // Varsayılan hızı kaydet

        uiManager = FindObjectOfType<UIManager_sc>(); // UIManager'ı bul
        uiManager.UpdateLivesUI(lives); // Başlangıç canını UI'de göster
    }

    void Update()
    {
        // Klavye girdilerini al
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); // Dikey hareket için giriş

        // Lazer ateşleme kontrolü
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    void FixedUpdate()
    {
        // Oyuncu hareketi
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;
        rb.velocity = movement;
    }

    void FireLaser()
    {
        if (isTripleShotActive)
        {
            // Üç lazeri farklı pozisyonlarda spawn et
            Vector3 leftLaserPosition = laserSpawnPoint.position + new Vector3(-0.5f, 0, 0);
            Vector3 rightLaserPosition = laserSpawnPoint.position + new Vector3(0.5f, 0, 0);

            Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity); // Orta lazer
            Instantiate(laserPrefab, leftLaserPosition, Quaternion.identity); // Sol lazer
            Instantiate(laserPrefab, rightLaserPosition, Quaternion.identity); // Sağ lazer
        }
        else
        {
            // Normal lazer
            Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ActivateTripleShot()
    {
        if (isTripleShotActive) return; // Zaten aktifse tekrar aktif etme

        isTripleShotActive = true;
        StartCoroutine(TripleShotTimer());
    }

    private IEnumerator TripleShotTimer()
    {
        yield return new WaitForSeconds(tripleShotDuration);
        isTripleShotActive = false;
        Debug.Log("Bonus Aktif Değil: TripleShot");
    }

    public void ActivateSpeedBoost()
    {
        if (isSpeedBoostActive) return; // Zaten aktifse tekrar etkinleştirme

        isSpeedBoostActive = true;
        speed *= 2; // Hızı iki katına çıkar
        StartCoroutine(SpeedBoostTimer());
    }

    private IEnumerator SpeedBoostTimer()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        speed = defaultSpeed; // Hızı varsayılan seviyeye geri döndür
        isSpeedBoostActive = false;
        Debug.Log("Bonus Aktif Değil: SpeedBoost");
    }

    public void LoseLife()
    {
        lives--; // Canı bir azalt
        if (lives <= 0)
        {
            lives = 0; // Negatif değerlere düşmesini engelle
            Debug.Log("Game Over!");
        }

        // UI'de can durumu güncelle
        if (uiManager != null)
        {
            uiManager.UpdateLivesUI(lives);
        }
    }

}