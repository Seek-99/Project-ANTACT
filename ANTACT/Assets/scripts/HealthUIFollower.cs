using UnityEngine;
using UnityEngine.UI;

public class HealthUIFollower : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (body_())
    public Vector3 offset = new Vector3(0f, 1.0f, 0f);
    public PlayerHealth playerHealth;

    private Text healthText;

    void Start()
    {
        healthText = GetComponentInChildren<Text>();

        if (healthText == null)
        {
            Debug.LogWarning("⚠️ Text 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        // ✅ 비활성화되었거나 Destroy 되었으면 UI도 파괴
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = target.position + offset;

        if (playerHealth != null)
        {
            healthText.text = $" {playerHealth.currentHealth} / {playerHealth.maxHealth}";
        }
    }
}
