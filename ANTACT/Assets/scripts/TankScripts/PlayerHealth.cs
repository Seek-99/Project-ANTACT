using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [Tooltip("�÷��̾��� �ִ� ü�� ��")]
    public int maxHealth = 100; // �ν����Ϳ��� ���� ����

    [Header("UI References")]
    [Tooltip("HP �� �����̴� ����")]
    public Slider healthSlider;



    [Tooltip("���� ü�� (�ν����Ϳ��� Ȯ�ο�)")]
    [SerializeField] public int currentHealth;



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

    }

    // ������ �޴� �Լ� �߰�
    public void TakeDamage(float amount)
    {
        currentHealth -= Mathf.RoundToInt(amount);
        Debug.Log("�÷��̾� �ǰ�! ���� ü��: " + currentHealth);

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
        Debug.Log("�÷��̾� ���!");
        Destroy(gameObject);
    }
    public int GetCurrentHealth() => currentHealth;

}