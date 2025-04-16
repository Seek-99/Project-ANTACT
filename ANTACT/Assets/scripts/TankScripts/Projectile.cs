using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime); 
    }

    // 충돌 시
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 시 파괴
        Debug.Log("포탄 충돌: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
