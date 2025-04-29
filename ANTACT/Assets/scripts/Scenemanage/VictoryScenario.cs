using UnityEngine;

public class VictoryScenario : MonoBehaviour
{
     public GameObject winUI;
     void Start()
    {
        winUI.SetActive(false); // 시작 시 숨김
    }

    void update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (enemies.Length == 0)
        {
            Debug.Log("Enemy 태그를 가진 오브젝트가 하나도 없습니다.");
            OnGameWin(); // 반대로 패배시 만들면 됨 
        
        }

        
    }
    public void OnGameWin()
    {
        Time.timeScale = 0f; // 게임 멈춤
        winUI.SetActive(true); // 승리 UI 표시
    }
}
