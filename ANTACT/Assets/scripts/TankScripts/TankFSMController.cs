using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Unity.MLAgents.Policies;
using Unity.VisualScripting;
using Unity.MLAgents;
using NavMeshPlus.Components;


public class TankFSMController : MonoBehaviour
{
    public TankBody tankBody;
    public TankTurret tankTurret;
    public Transform[] captureEntrances;
    public Transform captureZoneCenter;
    public TankAgent tankAgent;
    [SerializeField]
    Transform target;
    [SerializeField]
    NavMeshAgent navMeshAgent; // NavMeshAgent 컴포넌트

    private GameObject currentTargetEnemy;
    private Transform currentDestination;
    private bool inCombat = false;
    private bool wasShot = false;
    private bool arrived = false;

    private float lostEnemyTimer = 0f;
    private float scanDuration = 5f;

    private enum State { Navigate, Combat, Scan, Idle }
    private State currentState = State.Navigate;
    private GameObject currentTarget;
    private float rotationSpeed = 4f;

    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    private bool done = false;

    void Awake()
    {
        // 컴포넌트들을 캐싱
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    void Start()
    {
        SetNewDestination();
        if (tankBody == null)
            tankBody = GetComponentInChildren<TankBody>();

        if (tankTurret == null && tankBody != null)
            tankTurret = tankBody.GetComponentInChildren<TankTurret>();

        // NavMeshAgent 컴포넌트 초기화
        navMeshAgent = GetComponent<NavMeshAgent>();

        // NavMeshAgent의 회전 관련 설정 2d 환경이라 z축을 없애는 코드
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        Debug.Log("FSM스타트");
        Debug.Log($"FSM에이전트 : {tankAgent.name}");
    }

    void Update()
    {
        if(tankAgent.isDestroyed == true && done == false)
        {
            agent.enabled = false;
            obstacle.enabled = true;
            obstacle.carving = true;
            done = true;
        }
        currentTarget = tankAgent.currentTarget;
        if (currentTarget == null)
            Debug.Log($"FSM NULL1");
        else Debug.Log($"FSM {currentTarget}");
        // 공통 처리: 적 발견 또는 피격 → 전투 진입
        if (currentTarget != null)
        {
            currentTargetEnemy = currentTarget;
            currentState = State.Combat;
            inCombat = true;
        }
        if (wasShot == true)
        {
            currentState = State.Combat;
            inCombat = true;
            wasShot = false;
        }
        Debug.Log($"FSM,{currentState}");
        switch (currentState)
        {
            case State.Navigate:
                Debug.Log($"FSM네비");
                NavigateToTarget();
                break;

            case State.Combat:
                Debug.Log($"FSM전투"); 
                CombatBehavior();
                break;

            case State.Scan:
                Debug.Log($"FSM찾기"); 
                ScanForEnemies();
                break;

            case State.Idle:
                Debug.Log($"FSM도착"); 
                IdleInZone();
                break;
        }
    }

    void NavigateToTarget()
    {
        navMeshAgent.isStopped = false;
        target = currentDestination;
        Debug.Log($"{transform.name}의 목적지 {target}");
        navMeshAgent.SetDestination(target.position);
        LookAtMovementDirection();
        
        if (Vector2.Distance(transform.position, currentDestination.position) < 1f)
        {
            arrived = true; 
            Collider2D zoneCollider = captureZoneCenter.GetComponent<Collider2D>();
            if (zoneCollider != null)
            {
                currentDestination.position = GetRandomPointInCollider2D(zoneCollider);
            }

        }
        if (Vector2.Distance(transform.position, captureZoneCenter.position) < 1f)
        {
            currentState = State.Idle;
        }
    }
    Vector2 GetRandomPointInCollider2D(Collider2D collider)
    {
        Vector2 minBounds = collider.bounds.min;
        Vector2 maxBounds = collider.bounds.max;

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        Vector2 randomPoint = new Vector2(randomX, randomY);

        // 점이 콜라이더 내부인지 확인
        if (collider.OverlapPoint(randomPoint))
        {
            return randomPoint;
        }
        else
        {
            return GetRandomPointInCollider2D(collider); // 내부에 없으면 다시 시도
        }
    }

    void LookAtMovementDirection()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Debug.Log("FSM LOOK");
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 12f);
        }
        if (Vector2.SignedAngle(tankTurret.transform.up, transform.up) > 0)
        {
            tankTurret.HandleRotation(-2f);
        }
        else
        {
            tankTurret.HandleRotation(2f);
        }
    }


    void CombatBehavior()
    {
        navMeshAgent.isStopped = true;
        if (currentTarget == null)
        {
            lostEnemyTimer += Time.deltaTime;
            if (lostEnemyTimer >= 1f)
            {
                currentState = State.Scan;
                lostEnemyTimer = 0f;
            }
            return;
        }

        // 조준
        Vector2 dir = (currentTargetEnemy.transform.position - transform.position).normalized;
        if (Vector2.SignedAngle(tankTurret.transform.up, dir) > 0)
        {
            tankTurret.HandleRotation(-rotationSpeed);
        }
        else
        {
            tankTurret.HandleRotation(rotationSpeed);
        }

        if (Vector2.SignedAngle(transform.up, dir) > 0)
        {
            tankBody.Turn(-rotationSpeed);
        }
        else
        {
            tankBody.Turn(rotationSpeed);
        }

        float angle = Vector2.Angle(tankTurret.transform.up, dir);
        Debug.Log($"{angle} 각도FSM");
        if (angle < 2f)
        {
            Debug.Log($"FSM사격, {GetComponent<TankAgent>()}");
            tankTurret.Fire(GetComponent<TankAgent>());
        }
    }

    void ScanForEnemies()
    {
        Debug.Log($"{currentTargetEnemy},{currentTarget}FSM스캔");
        if (currentTargetEnemy.transform.parent.GetComponent<TankAgent>().isDestroyed == true)
        {
            currentState = State.Navigate;
            inCombat = false;
            lostEnemyTimer = 0f;
        }
        navMeshAgent.isStopped = false;
        lostEnemyTimer += Time.deltaTime;
        if (currentTargetEnemy)
        target = currentTargetEnemy.transform;
        navMeshAgent.SetDestination(target.position);

        // 이동 방향을 바라보게 하기
        LookAtMovementDirection();


        if (currentTarget != null)
        {
            currentState = State.Combat;
            lostEnemyTimer = 0f;
            return;
        }

        if (lostEnemyTimer >= scanDuration)
        {
            currentState = State.Navigate;
            inCombat = false;
            lostEnemyTimer = 0f;
        }
    }

    void IdleInZone()
    {
        // 점령지 도착 후 대기 및 포탑 회전만 수행
        tankTurret.HandleRotation(2f);
    }

    void SetNewDestination()
    {
        // 입구 선택
        currentDestination = captureEntrances[Random.Range(0, captureEntrances.Length)];
    }

    public void OnDamaged(GameObject attacker)
    {
        Debug.Log("FSM ondamaged 호출됨");
        wasShot = true;
        currentTargetEnemy = attacker.transform.GetChild(0).gameObject;
    }

    public void OnEnemyDestroyed()
    {
        currentTargetEnemy = null;
        inCombat = false;
        currentState = State.Navigate;
    }
}