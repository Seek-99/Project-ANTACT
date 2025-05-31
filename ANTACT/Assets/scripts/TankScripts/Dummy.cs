using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Policies;

public class Dummy : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [Tooltip("�÷��̾��� �ִ� ü�� ��")]
    public float maxHealth = 100f; 

    [Header("UI References")]
    [Tooltip("HP �� �����̴� ����")]
    public Slider healthSlider;



    [Tooltip("���� ü�� (�ν����Ϳ��� Ȯ�ο�)")]
    [SerializeField] public float currentHealth;

    public GameObject DiedTank; // 죽은 후 탱크 시체? 프리팹

    private int teamIndex;
    private Agent agent;

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
    {        // ML-Agents 에이전트 종료

        GameEndManager.TankDied(teamIndex);
        if (DiedTank != null)
        {
            Instantiate(DiedTank, transform.position, transform.rotation);
        }


        gameObject.SetActive(false); // 탱크 사망 처리
    }

    public float GetCurrentHealth() => currentHealth;

}