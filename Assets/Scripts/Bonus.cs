using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public enum BonusType { TripleShot, SpeedBoost } // Bonus türleri
    public BonusType bonusType; // Bonus türünü belirten değişken

    public float speed = 2f; // Aşağı doğru hareket hızı
    public float rotationSpeed = 20f; // Kendi ekseninde dönme hızı

    void Update()
    {
        // Bonus nesnesi aşağı doğru hareket eder
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Kendi ekseninde döner
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Eğer ekranın altına geçtiyse nesneyi yok et
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Lazerle çarpışma
        if (collision.CompareTag("Laser"))
        {
            ApplyBonus(); // Bonus etkisini uygula
            Destroy(collision.gameObject); // Lazer nesnesini yok et
            Destroy(gameObject); // Bonus nesnesini yok et
        }
    }

    void ApplyBonus()
    {
        Player player = FindObjectOfType<Player>();
        if (player == null) return; // Eğer oyuncu bulunamazsa işlem yapma

        // Bonus türüne göre etkisini uygula
        switch (bonusType)
        {
            case BonusType.TripleShot:
                Debug.Log("Bonus Aktif: TripleShot");
                player.ActivateTripleShot(); // Üçlü atış bonusunu aktif et
                break;

            case BonusType.SpeedBoost:
                Debug.Log("Bonus Aktif: SpeedBoost");
                player.ActivateSpeedBoost(); // Hız bonusunu aktif et
                break;

            default:
                Debug.LogWarning("Bilinmeyen bonus türü: " + bonusType);
                break;
        }
    }
}
