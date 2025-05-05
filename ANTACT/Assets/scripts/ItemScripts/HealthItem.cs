using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {

    // 태그로 플레이어만 필터링
    HealthStock healthstock = col.GetComponent<HealthStock>(); //HealthStock 스크립트 접근
        
    if (healthstock != null)
    {
        Debug.LogFormat("충돌 전 체력아이템 갯수: {0}", healthstock.HealthValue);

        if (healthstock.HealthValue > 3) //아이템 갯수가 2보다 많으면 회복 불가
        {
            healthstock.HealthValue = 2;
        }

        else
        {
            healthstock.HealthValue += 1;
        }
    }
    
    else
    {
        Debug.LogWarning("HealthStock을 찾을 수 없습니다");
    }
    Debug.LogFormat("회복 후 체력아이템 갯수: {0}", healthstock.HealthValue);
        
    
    }
}