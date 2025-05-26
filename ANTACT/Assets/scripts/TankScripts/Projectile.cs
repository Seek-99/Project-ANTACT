using UnityEngine;
using Unity.MLAgents;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] public float damage = 30f; // 기본 데미지

    public GameObject explosionEffectPrefab; // 피격 이펙트

    private Rigidbody2D rb;
    public Agent owner; // 발사한 에이전트

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    // 충돌 시
private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log($"충돌한 객체: {collision.gameObject.name}, 태그: {collision.gameObject.tag}, 부모: {collision.gameObject.transform.parent}");

    // 🔒 건물 태그와 충돌하면 아무 처리도 하지 않고 바로 리턴
    if (collision.gameObject.CompareTag("Object"))
    {
        Debug.Log("건물이므로 충돌 무시됨");
        Destroy(gameObject); // 총알은 여전히 제거
        return;
    }

    // 피격 대상에게 데미지 부여
    var damageable = collision.gameObject.GetComponent<IDamageable>();
    if (damageable != null)
    {
        damageable.TakeDamage(damage);
        if (collision.gameObject.GetComponentInParent<TankAgent>() != null)
        {
            collision.gameObject.GetComponentInParent<TankAgent>().AddReward(-2f); // 피격 시 페널티
            Debug.Log($"{collision.gameObject.transform.parent}: 피격! - 발사자: {owner}, Reward: -2");
        }
        else
        {
            Debug.Log("충돌한 객체에 TankAgent가 없습니다.");
        }
    }

    // 보상/페널티 처리
    if (owner != null)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            owner.AddReward(+2f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            owner.AddReward(-3f);
        }
    }

    if (explosionEffectPrefab != null)
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }
    else
    {
        Debug.LogWarning("폭발 이펙트 프리팹이 설정되지 않았습니다.");
    }

    Destroy(gameObject); // 충돌 시 파괴
}

}
