using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [Tooltip("플레이어의 최대 체력 값")]
    public int maxHealth = 100; // 인스펙터에서 조절 가능

    [Header("UI References")]
    [Tooltip("HP 바 슬라이더 참조")]
    public Slider healthSlider;



    [Tooltip("현재 체력 (인스펙터에서 확인용)")]
    [SerializeField] private int currentHealth;



    void Start()
    {
        InitializeHealth();
    }

    
    /// 체력 시스템 초기화

    private void InitializeHealth()
    {
        currentHealth = maxHealth;

        // 슬라이더 설정
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider가 할당되지 않았습니다!");
        }
    }

    // 데미지 받는 함수 추가
    public void TakeDamage(float amount)
    {
        currentHealth -= Mathf.RoundToInt(amount);
        Debug.Log("플레이어 피격! 남은 체력: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망!");
        Destroy(gameObject);
    }
    public int GetCurrentHealth() => currentHealth;

}