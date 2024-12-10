using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f; // Lazerin hareket hızı

    void Update()
    {
        // Yukarı doğru hareket
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Eğer lazer ekranın dışına çıkarsa yok et
        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer lazer bir düşmanla çarpışırsa
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Düşmanı yok et
            Destroy(gameObject); // Lazerin kendisini yok et

            // UI Manager'dan puanı artır
            UIManager_sc uiManager = FindObjectOfType<UIManager_sc>();
            if (uiManager != null)
            {
                uiManager.UpdateScore(10); // Puanı 10 artır
            }
        }
    }

}
