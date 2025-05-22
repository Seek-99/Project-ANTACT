using UnityEngine;

public class VictoryCircle : MonoBehaviour
{
    public float pointPerTick = 1f;        // 1초마다 오를/내릴 점수
    public float VictoryPoint = 0f;        // 현재 점수 상태
    public float tickInterval = 1f;        // 점수 계산 주기 (초)

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            timer = 0f;

            int playerCount = 0;
            int enemyCount = 0;

            // **현재 오브젝트의 실제 크기를 기준으로 반지름 계산**
            // 오브젝트의 로컬 스케일이 아니라, 월드 기준 스케일을 사용 (lossyScale)
            float radius = transform.lossyScale.x / 2f;

            // "Player" 오브젝트 찾기
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in players)
            {
                if (Vector3.Distance(obj.transform.position, transform.position) <= radius)
                    playerCount++;
            }

            // "Enemy" 오브젝트 찾기
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in enemies)
            {
                if (Vector3.Distance(obj.transform.position, transform.position) <= radius)
                    enemyCount++;
            }

            // 점수 증가/감소 로직
            if (playerCount > 0 && enemyCount == 0)
            {
                VictoryPoint += pointPerTick;
            }
            else if (enemyCount > 0 && playerCount == 0)
            {
                VictoryPoint -= pointPerTick;
            }

            Debug.Log($"VictoryPoint: {VictoryPoint}");
        }
    }
}