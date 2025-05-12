using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text healthText;
    public Text apText;

    public Text heText;

    public Text healthItemText;

    public PlayerHealth playerHealth;
    public AmmunityStock ammunityStock;

    public HealthStock healthStock;

    private float AP; 

    private float HE;

    private float HealthItemValue;

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
            SetHE(ammunityStock.GetCurrentHE());
        }
        else
        {
            Debug.LogWarning("AmmunityStock 컴포넌트를 찾을 수 없습니다!");
        }
        if (healthStock != null)
        {
            SetHealthValue(healthStock.GetCurrentHealthValue());
        }
        else
        {
            Debug.LogWarning("HealthStock 컴포넌트를 찾을 수 없습니다!");
        }
    }

    

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        UpdateHealthText();
    }

    public void SetHealth(float health)
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

    private void SetAP(float ap)
    {
        AP = ap;
        UpdateAP();
    }

    private void SetHE(float he)
    {
        HE = he;
        UpdateHE();
    }
    
    private void SetHealthValue(float heatlhvalue)
    {
        HealthItemValue = heatlhvalue;
        UpdateHealthItemValue();
    }

    private void UpdateAP()
    {
        if (apText != null)
        {
            apText.text = $"{AP}";
        }
    }

    private void UpdateHE()
    {
        if (heText != null)
        {
            heText.text = $"{HE}";
        }
    }

    private void UpdateHealthItemValue()
    {
        if (healthItemText != null)
        {
            healthItemText.text = $"{HealthItemValue}";
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
            SetHE(ammunityStock.GetCurrentHE());
        }
        
        if (healthStock != null)
        {
            SetHealthValue(healthStock.GetCurrentHealthValue());
        }
    }
}
