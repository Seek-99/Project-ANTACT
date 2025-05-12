using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Policies;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [Tooltip("�÷��̾��� �ִ� ü�� ��")]
    public float maxHealth = 100f; 

    [Header("UI References")]
    [Tooltip("HP �� �����̴� ����")]
    public Slider healthSlider;



    [Tooltip("���� ü�� (�ν����Ϳ��� Ȯ�ο�)")]
    [SerializeField] public float currentHealth;


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
    {
        // ML-Agents 에이전트 종료
        var agent = GetComponentInParent<TankAgent>();
        agent.isDestroyed = true; // 에이전트 사망 처리
        Debug.Log($"{agent.name} 사망");
        // 팀별 탱크 사망 처리

        if (agent != null)
        {
            // Behavior Parameters에서 팀 정보 가져오기
            teamIndex = agent.GetComponent<BehaviorParameters>().TeamId;
        }
        else
        {
            Debug.LogError("Agent 컴포넌트를 찾을 수 없습니다.");
        }

        GameEndManager.TankDied(teamIndex);

        gameObject.SetActive(false); // 탱크 사망 처리
    }

    public float GetCurrentHealth() => currentHealth;

}