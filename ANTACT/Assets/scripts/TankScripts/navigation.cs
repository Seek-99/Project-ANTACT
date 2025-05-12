using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class navigation : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    NavMeshAgent navMeshAgent; // NavMeshAgent 컴포넌트

    private bool isDestroyed = false;

    void Start()
    {
        // NavMeshAgent 컴포넌트 초기화
        navMeshAgent = GetComponent<NavMeshAgent>();

        // NavMeshAgent의 회전 관련 설정 2d 환경이라 z축을 없애는 코드
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }


    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player != null)
        {
            target = player.transform;
            navMeshAgent.SetDestination(target.position);
        }
        // 이동 방향을 바라보게 하기
        LookAtMovementDirection();
    }

    void LookAtMovementDirection()
    {
        // NavMeshAgent의 속도를 기준으로 방향 계산
        Vector3 velocity = navMeshAgent.velocity;

        // 속도가 있을 때만 회전 처리
        if (velocity.sqrMagnitude > 0.01f) // 속도가 거의 0이 아니면
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 감지됨: " + other.gameObject.name); // 충돌 감지 확인
        if (other.CompareTag("Player")) // 플레이어 전차와 충돌 시 삭제 & HQ에서 새 차량 소환
        {
            Debug.Log("플레이어 전차와 충돌! 차량 삭제 진행");     
            Destroy(gameObject);
        }
    }
}
