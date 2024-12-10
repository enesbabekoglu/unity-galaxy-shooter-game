using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f; // Düşman hareket hızı
    public float rotationSpeed = 20f; // Düşman dönüş hızı

    void Update()
    {
        // Aşağı doğru hareket
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Kendi ekseninde dönme
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // Eğer ekranın altına geçtiyse
        if (transform.position.y < -6f)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.LoseLife(); // Oyuncunun canını eksilt
            }

            Destroy(gameObject); // Düşmanı yok et
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Oyuncuyla çarpışma
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.LoseLife(); // Oyuncunun canını eksilt
            }

            Destroy(gameObject); // Düşmanı yok et
        }
    }
}
