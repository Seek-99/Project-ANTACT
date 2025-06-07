using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Text healthText;
    public Text apText;
    public Text heText;
    public Text healthItemText;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

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
            SetMaxHealth(playerHealth.maxHealth);

        if (playerHealth != null)
            SetHealth(playerHealth.GetCurrentHealth());

        if (ammunityStock != null)
        {
            SetAP(ammunityStock.GetCurrentAP());
            SetHE(ammunityStock.GetCurrentHE());
        }

        if (healthStock != null)
            SetHealthValue(healthStock.GetCurrentHealthValue());
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
            healthText.text = $"{slider.value} / {slider.maxValue}";
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

    private void SetHealthValue(float healthValue)
    {
        HealthItemValue = healthValue;
        UpdateHealthItemValue();
    }

    private void UpdateAP()
    {
        if (apText != null)
            apText.text = $"{AP}";
    }

    private void UpdateHE()
    {
        if (heText != null)
            heText.text = $"{HE}";
    }

    private void UpdateHealthItemValue()
    {
        if (healthItemText != null)
            healthItemText.text = $"{HealthItemValue}";
    }

    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            UpdateReferences(player1);
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            UpdateReferences(player2);
        }
        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            UpdateReferences(player3);
        }

        if (playerHealth != null)
            SetHealth(playerHealth.GetCurrentHealth());

        if (ammunityStock != null)
        {
            SetAP(ammunityStock.GetCurrentAP());
            SetHE(ammunityStock.GetCurrentHE());
        }

        if (healthStock != null)
            SetHealthValue(healthStock.GetCurrentHealthValue());
    }

    private void UpdateReferences(GameObject player)
    {
        if (player == null)
            return;

        Transform body = player.transform.Find("body_0");
        if (body == null)
            return;

        PlayerHealth newHealth = body.GetComponent<PlayerHealth>();
        if (newHealth != null)
        {
            playerHealth = newHealth;
            SetMaxHealth(playerHealth.maxHealth);
        }

        AmmunityStock newAmmo = body.GetComponent<AmmunityStock>();
        if (newAmmo != null)
        {
            ammunityStock = newAmmo;
        }

        HealthStock newHealthStock = body.GetComponent<HealthStock>();
        if (newHealthStock != null)
        {
            healthStock = newHealthStock;
        }
    }
}
