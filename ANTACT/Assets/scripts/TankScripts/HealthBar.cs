using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text healthText;

    public PlayerHealth playerHealth;
    void Start()
    {
        slider.interactable = false;


        if (playerHealth != null)
        {
            SetMaxHealth(playerHealth.maxHealth);
            SetHealth(playerHealth.GetCurrentHealth());
        }
        else
        {
            Debug.LogWarning("PlayerHealth ������Ʈ�� ã�� �� �����ϴ�!");
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

    // �� ������ ü�� ������Ʈ 
    void Update()
    {
        if (playerHealth != null)
        {
            SetHealth(playerHealth.GetCurrentHealth());
        }
    }
}

