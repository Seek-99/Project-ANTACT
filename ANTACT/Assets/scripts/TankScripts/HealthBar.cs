using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text healthText;
    public Text apText;

    public PlayerHealth playerHealth;
    public AmmunityStock ammunityStock;

    private int AP; // 👈 추가된 부분

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
            Debug.LogWarning("PlayerHealth 컴포넌트를 찾을 수 없습니다!");
        }

        if (ammunityStock != null)
        {
            SetAP(ammunityStock.GetCurrentAP());
        }
        else
        {
            Debug.LogWarning("AmmunityStock 컴포넌트를 찾을 수 없습니다!");
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

    private void SetAP(int ap)
    {
        AP = ap;
        UpdateAP();
    }

    private void UpdateAP()
    {
        if (apText != null)
        {
            apText.text = $"{AP}";
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            SetHealth(playerHealth.GetCurrentHealth());
        }

        if (ammunityStock != null)
        {
            SetAP(ammunityStock.GetCurrentAP());
        }
    }
}
