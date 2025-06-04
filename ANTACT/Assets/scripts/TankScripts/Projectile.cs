using UnityEngine;
using Unity.MLAgents;
using UnityEngine.InputSystem.XR;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] public float damage = 30f;

    public GameObject explosionEffectPrefab;

    private Rigidbody2D rb;

    public Agent owner;                  // 발사한 전차 (TankAgent)
    public AmmunityStock ammuStock;      // 발사자 전차의 정확한 탄약 정보

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"충돌한 객체: {collision.gameObject.name}, 태그: {collision.gameObject.tag}, 부모: {collision.gameObject.transform.parent}");

        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("건물이므로 충돌 무시됨");
            Destroy(gameObject);
            return;
        }

        var damageable = collision.gameObject.GetComponent<IDamageable>();

        // AmmunityStock에서 정확한 정보 가져오기
        float multiple = 1.0f;
        string ammoStatus = "ap";

        if (ammuStock != null)
        {
            multiple = ammuStock.Multiple;
            ammoStatus = ammuStock.status;
        }
        else
        {
            Debug.LogWarning("Projectile에 AmmunityStock이 연결되지 않았습니다.");
        }

        // 데미지 계산 및 적용
        if (damageable != null)
        {
            float finalDamage = (ammoStatus == "he") ? damage * multiple * 1.5f : damage * multiple;
            damageable.TakeDamage(finalDamage);

            var hitAgent = collision.gameObject.GetComponentInParent<TankAgent>();
            if (hitAgent != null)
            {
                hitAgent.AddReward(-2f);
                Debug.Log($"{collision.gameObject.transform.parent}: 피격! - 발사자: {owner}, Reward: -2");
                collision.gameObject.GetComponentInParent<TankAgent>().controller.OnDamaged(owner.gameObject);
            }
            else
            {
                Debug.Log("충돌한 객체에 TankAgent가 없습니다.");
            }
        }

        // 명중 보상
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

        Destroy(gameObject);
    }
}


