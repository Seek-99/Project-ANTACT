using UnityEngine;
using UnityEngine.AI;

public class navigation : MonoBehaviour
{
    [SerializeField]
    Transform target; // 목표 Transform
    [SerializeField]
    Transform HQ; // 목표 Transform2
    private int previousdestination = 0;
    public float diversionDistance = 10f;
    [SerializeField]
    NavMeshAgent navMeshAgent; // NavMeshAgent 컴포넌트

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
        //전차와 보급차량 사이의 거리
        

        // NavMeshAgent로 목적지 설정
        if (previousdestination == 0)
        {
            float distanceToTank = Vector2.Distance(transform.position, target.position);
            GameObject player = GameObject.FindGameObjectWithTag("player");
            if (player != null)
            {
                target = player.transform;
                navMeshAgent.SetDestination(target.position);
            }
            if (distanceToTank <= diversionDistance)
            {
                previousdestination = 1;
            }
            ;
        }
        else if (previousdestination == 1)
        {
            float distanceToTank = Vector2.Distance(transform.position, HQ.position);
            navMeshAgent.SetDestination(HQ.position);
            if (distanceToTank <= diversionDistance)
            {
                previousdestination = 0;
            }
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
}
