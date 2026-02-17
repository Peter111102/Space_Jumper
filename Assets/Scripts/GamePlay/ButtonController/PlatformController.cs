using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private PlatformData data;
    private Rigidbody2D rb;
    private float timer = 4f;

    public void Setup(PlatformData newData)
    {
        data = newData;
        GetComponent<SpriteRenderer>().sprite = data.mainSprite;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)   rb.bodyType = RigidbodyType2D.Dynamic; 


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se il player tocca la piattaforma dall'alto
        if (collision.relativeVelocity.y <= 0.1f && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * data.jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("BlackHole"))
        {
            data.SpawnDebris(transform.position); // Chiamata al Data
            gameObject.SetActive(false);
        }
    }
}