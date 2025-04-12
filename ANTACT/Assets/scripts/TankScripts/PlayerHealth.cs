using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("�÷��̾��� �ִ� ü�� ��")]
    public int maxHealth = 100; // �ν����Ϳ��� ���� ����

    [Header("UI References")]
    [Tooltip("HP �� �����̴� ����")]
    public Slider healthSlider;

    private int currentHealth;

    void Start()
    {
        InitializeHealth();
    }

    
    /// ü�� �ý��� �ʱ�ȭ

    private void InitializeHealth()
    {
        currentHealth = maxHealth;

        // �����̴� ����
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider�� �Ҵ���� �ʾҽ��ϴ�!");
        }
    }


}