using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text healthText;

    private PlayerHealth playerHealth;  // PlayerHealth 참조

    void Start()
    {
        slider.interactable = false;

        // 부모 오브젝트에서 PlayerHealth 찾기
        playerHealth = GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            // 초기 체력 설정
            SetMaxHealth(playerHealth.maxHealth);
            SetHealth(playerHealth.GetCurrentHealth());
        }
        else
        {
            Debug.LogWarning("PlayerHealth 컴포넌트를 찾을 수 없습니다!");
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        UpdateHealthText();
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{slider.value} / {slider.maxValue}";
        }
    }

    // 매 프레임 체력 업데이트 
    void Update()
    {
        if (playerHealth != null)
        {
            SetHealth(playerHealth.GetCurrentHealth());
        }
    }
}

