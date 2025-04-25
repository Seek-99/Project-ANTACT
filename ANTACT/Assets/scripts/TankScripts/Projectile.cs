using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float damage = 30f; // 기본 데미지

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed * Time.deltaTime;
        Destroy(gameObject, lifeTime); 
    }

    // 충돌 시
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("포탄 충돌: " + collision.gameObject.name);

        // 피격 대상에게 데미지 부여
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        Destroy(gameObject); // 충돌 시 파괴
    }
}
