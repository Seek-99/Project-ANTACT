using UnityEngine;
using Unity.MLAgents;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    public VictoryCircle victorycircle;

    private bool isGameEnded = false;
    private static int[] aliveTankCount = { 3, 3 }; // 팀 0과 팀 1의 살아있는 탱크 수

    void Start()
    {
        if (winUI == null) Debug.LogError(" winUI가 할당되지 않았습니다.");
        else winUI.SetActive(false);

        if (loseUI == null) Debug.LogError(" loseUI가 할당되지 않았습니다.");
        else loseUI.SetActive(false);

        // 씬 로드 시 모든 에이전트 활성화
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (isGameEnded) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Debug.Log($" Enemy 수: {enemies.Length}, Player 수: {players.Length}");

        if (victorycircle != null)
        {
            if (victorycircle.VictoryPoint >= 100f)
            {
                Debug.Log("점령 승리 조건 달성!");
                ShowWinUI();
            }
            else if (victorycircle.VictoryPoint <= -100f)
            {
                Debug.Log("점령 패배 조건 달성!");
                ShowLoseUI();
            }
        }

        if (enemies.Length == 0 && players.Length > 0)
        {
            Debug.Log("승리 조건 달성!");
            ShowWinUI();
        }
        // 패배 조건: 팀 1의 탱크가 모두 죽고, 팀 0의 탱크는 살아있는 상태
        else if (players.Length == 0)
        {
            Debug.Log("패배 조건 달성!");
            ShowLoseUI();
        }
    }

    public static void TankDied(int teamIndex)
    {
        aliveTankCount[teamIndex]--;
        Debug.Log($"팀 {teamIndex} 남은 탱크 수: {aliveTankCount[teamIndex]}");
    }

    void ShowWinUI()
    {
        if (winUI != null) winUI.SetActive(true);
        EndGame();
    }

    void ShowLoseUI()
    {
        if (loseUI != null) loseUI.SetActive(true);
        EndGame();
    }

    void EndGame()
    {
        isGameEnded = true;
        Debug.Log("게임 종료");

        /*
        // 모든 에이전트 에피소드 종료
        Debug.Log("에피소드 종료");
        EndAllAgents();

        // 학습 모드면 자동 리셋, 게임 모드면 UI 유지
        Debug.Log("게임 종료 후 리셋 대기 시작");
        StartCoroutine(ResetGameAfterDelay(2f));
        */
    }

    IEnumerator ResetGameAfterDelay(float delay)
    {
        Debug.Log($"리셋 대기 중... {delay}초 기다림");
        yield return new WaitForSeconds(delay);

        // UI 숨기기
        if (winUI != null)
        {
            winUI.SetActive(false);
            Debug.Log("승리 UI 숨기기");
        }
        if (loseUI != null)
        {
            loseUI.SetActive(false);
            Debug.Log("패배 UI 숨기기");
        }

        isGameEnded = false;
        Debug.Log("게임 상태 초기화 완료");

        // 방법 1: 현재 씬 재로드 (완전 초기화)
        Debug.Log("현재 씬 재로드 시작");
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        Debug.Log("씬 재로드 완료");
    }

    void EndAllAgents()
    {
        var agents = Object.FindObjectsByType<Unity.MLAgents.Agent>(FindObjectsSortMode.None);
        foreach (var agent in agents)
        {
            if (agent != null)
            {
            agent.gameObject.SetActive(true);
            agent.EndEpisode();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var agents = Object.FindObjectsByType<Unity.MLAgents.Agent>(FindObjectsSortMode.None);
        foreach (var agent in agents)
        {
            agent.gameObject.SetActive(true); // 에이전트 활성화
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 OnSceneLoaded 호출
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트 리스너 제거
    }
}
