using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private int healAmount = 30; // 회복량

    private void OnTriggerEnter2D(Collider2D col)
{

    // 태그로 플레이어만 필터링
    if (col.CompareTag("player"))
    {
        PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            int beforeHealth = playerHealth.currentHealth;
            Debug.LogFormat("충돌 전 체력: {0}", beforeHealth);

            playerHealth.currentHealth += healAmount;

            if (playerHealth.currentHealth > playerHealth.maxHealth)
                playerHealth.currentHealth = playerHealth.maxHealth;

            if (playerHealth.healthSlider != null)
                playerHealth.healthSlider.value = playerHealth.currentHealth;

            Debug.LogFormat("회복 후 체력: {0}", playerHealth.currentHealth);
        }
    }
}
}