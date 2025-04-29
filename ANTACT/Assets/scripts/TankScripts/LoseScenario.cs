using UnityEngine;

public class LoseScenario : MonoBehaviour
{
     public GameObject loseUI;
     void Start()
    {
        loseUI.SetActive(false); // 시작 시 숨김
    }

    void update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.Log("Player 태그를 가진 오브젝트가 하나도 없습니다.");
            OnGameLose(); // 반대로 패배시 만들면 됨 
        
        }

        
    }
    public void OnGameLose()
    {
        Time.timeScale = 0f; // 게임 멈춤
        loseUI.SetActive(true); // 패배배 UI 표시
    }
}
