using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    private bool isGameEnded = false;

    void Start()
    {
        if (winUI == null) Debug.LogError(" winUI가 할당되지 않았습니다.");
        else winUI.SetActive(false);

        if (loseUI == null) Debug.LogError(" loseUI가 할당되지 않았습니다.");
        else loseUI.SetActive(false);
    }

    void Update()
    {
        if (isGameEnded) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");

        Debug.Log($" Enemy 수: {enemies.Length}, Player 수: {players.Length}");

        if (enemies.Length == 0 && players.Length > 0)
        {
            Debug.Log(" 승리 조건 달성!");
            ShowWinUI();
        }
        else if (players.Length == 0)
        {
            Debug.Log(" 패배 조건 달성!");
            ShowLoseUI();
        }
    }

    void ShowWinUI()
    {
        if (winUI != null)
        {
            winUI.SetActive(true);
            Debug.Log(" winUI 표시됨");
        }

        Time.timeScale = 0f;
        isGameEnded = true;
    }

    void ShowLoseUI()
    {
        if (loseUI != null)
        {
            loseUI.SetActive(true);
            Debug.Log(" loseUI 표시됨");
        }

        Time.timeScale = 0f;
        isGameEnded = true;
    }
}
