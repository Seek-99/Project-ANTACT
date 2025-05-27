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
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            if (player1 != null)
            {
                Transform body = player1.transform.Find("body_0");
                if (body != null)
                {
                    PlayerHealth newHealth = body.GetComponent<PlayerHealth>();
                    if (newHealth != null)
                    {
                        playerHealth = newHealth;
                        SetMaxHealth(playerHealth.maxHealth); // 슬라이더도 다시 설정
                        Debug.Log("F1 탱크의 Body에서 PlayerHealth 연결 완료");
                    }
                    else
                    {
                        Debug.LogWarning("Body 오브젝트에 PlayerHealth 컴포넌트가 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("f1Tank에 'Body' 하위 오브젝트를 찾을 수 없습니다.");
                }
            }
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            if (player2 != null)
            {
                Transform body = player2.transform.Find("body_0");
                if (body != null)
                {
                    PlayerHealth newHealth = body.GetComponent<PlayerHealth>();
                    if (newHealth != null)
                    {
                        playerHealth = newHealth;
                        SetMaxHealth(playerHealth.maxHealth); // 슬라이더도 다시 설정
                        Debug.Log("F2 탱크의 Body에서 PlayerHealth 연결 완료");
                    }
                    else
                    {
                        Debug.LogWarning("Body 오브젝트에 PlayerHealth 컴포넌트가 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("f2Tank에 'Body' 하위 오브젝트를 찾을 수 없습니다.");
                }
            }
        }
        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            if (player3 != null)
            {
                Transform body = player3.transform.Find("body_0");
                if (body != null)
                {
                    PlayerHealth newHealth = body.GetComponent<PlayerHealth>();
                    if (newHealth != null)
                    {
                        playerHealth = newHealth;
                        SetMaxHealth(playerHealth.maxHealth); // 슬라이더도 다시 설정
                        Debug.Log("F3 탱크의 Body에서 PlayerHealth 연결 완료");
                    }
                    else
                    {
                        Debug.LogWarning("Body 오브젝트에 PlayerHealth 컴포넌트가 없습니다.");
                    }
                }
                else
                {
                    Debug.LogWarning("f3Tank에 'Body' 하위 오브젝트를 찾을 수 없습니다.");
                }
            }
        }


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