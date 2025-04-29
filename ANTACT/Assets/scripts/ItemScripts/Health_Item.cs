using UnityEngine;

public class Health_Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("player"))
        {
            //PlayerHealth Tank1 = col.GetComponent<PlayerHealth>();
            PlayerHealth PlayerHealth_Script = col.GetComponent<PlayerHealth>();
            if (PlayerHealth_Script != null)
            {
            PlayerHealth_Script.currentHealth += 30;
            if (PlayerHealth_Script.currentHealth > PlayerHealth_Script.maxHealth)
                PlayerHealth_Script.currentHealth = PlayerHealth_Script.maxHealth;
            Destroy(gameObject);
            Debug.Log("Destroy");
            }
            else
            {
                Debug.LogWarning("PlayerHealth 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
            {
                Debug.LogWarning("Player 태그를 찾을 수 없습니다.");
            }
    }
}
