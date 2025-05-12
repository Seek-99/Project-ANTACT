using UnityEngine;

public class BodyHitRate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Projectile 판별 (태그 방식)
        if (!collision.gameObject.CompareTag("Untagged"))
            {
                Debug.Log("Projectile을 찾지 못했습니다");
                return;
            }

        // 2. 충돌 지점 획득
        Vector2 hitPoint = collision.GetContact(0).point;

        // 3. 로컬 좌표 변환
        Vector2 localPoint = transform.InverseTransformPoint(hitPoint);

        // 4. 각도 계산
        float angle = Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;

        //데미지 설정
        float damageMultiplier = 1f;

        // 5. 각도에 따라 방향 판별 및 데미지 배율 설정
        //위
        if (angle >= -30f && angle < 30f)
        {
            damageMultiplier = 1.0f;
            Debug.Log("앞쪽 피격");
        }
        //왼쪽
        else if (angle >= 30f && angle < 150f)
        {
            damageMultiplier = 1.3f;
            Debug.Log("왼쪽 피격");
        }
        //밑
        else if (angle >= 150f || angle < -150f)
        {
            damageMultiplier = 1.5f;
            Debug.Log("밑에 피격");
        }
        // 왼쪽
        else
        {
            damageMultiplier = 1.3f;
            Debug.Log("왼쪽 피격");
        }

        // 6. 실제 데미지 적용
        ApplyDamage(damageMultiplier);
    }

    void ApplyDamage(float damage)
    {
        GetComponent<PlayerHealth>().currentHealth -= GetComponent<AmmunityStock>().AP * damage;
        Debug.Log($"데미지 {damage}배 적용, 남은 체력: {GetComponent<PlayerHealth>().currentHealth}");
    }
}