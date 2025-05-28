using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using System.Collections.Generic;

public class TankAgent : Agent
{
    public TankBody tankBody;
    public TankTurret turret;
    public Transform turretTransform;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float viewAngle = 150f;
    public float viewDistance = 60f;
    [SerializeField] private float fireCooldown = 2f; // 쿨다운 시간 (초)
    private float lastFireTime = -Mathf.Infinity;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public LayerMask coverLayer;

    private GameObject currentTarget;

    public bool isDestroyed = false;
    public bool isPlayerControlled = false;

    private int teamId;

    public override void Initialize()
    {
        base.Initialize();

        var behaviorParams = GetComponent<BehaviorParameters>();
        if (behaviorParams != null)
        {
            teamId = behaviorParams.TeamId;
            Debug.Log($"[{gameObject.name}] Team ID: {teamId}");

            // 팀에 따라 적 레이어 설정
            enemyLayer = teamId == 0 ? LayerMask.GetMask("EnemyTank") : LayerMask.GetMask("PlayerTank");
        }
        else
        {
            Debug.LogError("BehaviorParameters 컴포넌트를 찾을 수 없습니다!");
        }

        obstacleLayer = LayerMask.GetMask("fov");
        coverLayer = LayerMask.GetMask("fov");

        // 자식 오브젝트 자동 참조 설정
        if (tankBody == null)
            tankBody = GetComponentInChildren<TankBody>();

        if (turret == null && tankBody != null)
            turret = tankBody.GetComponentInChildren<TankTurret>();

        if (turretTransform == null && turret != null)
            turretTransform = turret.transform;

        if (firePoint == null && turretTransform != null)
            firePoint = turretTransform.Find("FirePoint");

        if (projectilePrefab == null)
            projectilePrefab = Resources.Load<GameObject>("Projectile");
    }

    public override void OnEpisodeBegin()
    {
        // 초기화
        Debug.Log("에피소드 시작");
        tankBody.Reset();
        isDestroyed = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (isPlayerControlled | isDestroyed) return;
        // 자신의 전차 방향, 속도
        sensor.AddObservation(transform.up);
        sensor.AddObservation(tankBody.GetVelocity());

        // 시야 내 적 감지
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, viewDistance, enemyLayer);
        bool foundEnemy = false;

        foreach (var enemy in enemies)
        {
            Vector2 toEnemy = (enemy.transform.position - transform.position).normalized;
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            float angle = Vector2.Angle(transform.up, toEnemy);

            if (CanSeeTarget(enemy.gameObject))
            {
                sensor.AddObservation(toEnemy); // 방향
                sensor.AddObservation(dist);    // 거리
                foundEnemy = true;
                currentTarget = enemy.gameObject;

                // 거리가 가까울수록 보상 (거리 100 이상: 0점, 20 이하: 1점)
                float proximityReward = Mathf.Clamp01(Mathf.Pow((100f - dist) / 80f, 2));
                AddReward(proximityReward * 0.05f); // 거리 기반 보상

                // 포탑이 적을 바라보는 방향에 가까울수록 보상 추가
                float turretAngle = Vector2.Angle(turretTransform.up, toEnemy); // 포탑이 적을 향하는 각도
                float turretAimReward = Mathf.Clamp01(1f - Mathf.Pow(turretAngle / 180f, 2)); // 각도가 작을수록 보상 증가
                AddReward(turretAimReward * 0.05f); // 포탑 방향에 대한 보상

                Debug.Log($"{gameObject.name}: 적 발견 - {enemy.transform.parent}, 거리: {dist:F2}, 각도: {angle:F1}°, 포탑 각도: {turretAngle:F1}°, 보상: {turretAimReward * 0.05f}");

                break;
            }
            else
            {
                Debug.Log($"{gameObject.name}: 적 발견 - {enemy.transform.parent}, 거리: {dist:F2}, 각도: {angle:F1}° (시야각 외부)");
            }
        }

        if (!foundEnemy)
        {
            sensor.AddObservation(Vector2.zero); // 방향
            sensor.AddObservation(0f);           // 거리
            currentTarget = null;
        }
        /*가장 가까운 엄폐물 방향
        Collider2D cover = Physics2D.OverlapCircle(transform.position, viewDistance, coverLayer);
        if (cover && CanSeeTarget(cover.gameObject))
        {
            Vector2 coverDir = (cover.transform.position - transform.position).normalized;
            sensor.AddObservation(coverDir);
        }
        else
        {
            sensor.AddObservation(Vector2.zero);
        }*/
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // 파괴되지 않은 경우에만 행동을 처리
        if (isPlayerControlled | isDestroyed) return;

        Debug.Log($"name: {gameObject.name}, move: {actions.ContinuousActions[0]}, turn: {actions.ContinuousActions[1]}, shoot: {actions.ContinuousActions[2]}, turretTurn: {actions.ContinuousActions[3]}");
        float move = Mathf.Clamp(actions.ContinuousActions[0], -0.5f, 1.5f);
        float turn = Mathf.Clamp(actions.ContinuousActions[1], -1f, 1f);
        float shoot = Mathf.Clamp(actions.ContinuousActions[2], 0f, 1f);
        float turretTurn = Mathf.Clamp(actions.ContinuousActions[3], -1f, 1f); // 포탑 회전

        tankBody.Move(move);
        tankBody.Turn(turn);
        turret.HandleRotation(turretTurn); // 포탑 회전

        if (shoot > 0.1f)
        {
            TryShoot();
        }

        // 살아있다면 시간당 페널티
        AddReward(-0.001f);
    }

    private void TryShoot()
    {
        if (Time.time < lastFireTime + fireCooldown)
        {
            Debug.Log($"{gameObject.name}: 사격 실패 - 쿨다운 중");
            return;
        }

        if (currentTarget == null)
        {
            Debug.Log($"{gameObject.name}: 사격 실패 - 타겟 없음");
            return;
        }

        Vector2 fireDir = (currentTarget.transform.position - firePoint.position).normalized;
        float angle = Vector2.Angle(turretTransform.up, fireDir);
        if (angle < 5f)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projScript = proj.GetComponent<Projectile>();
            projScript.owner = this; // 누가 발사했는지 지정
            projScript.ammuStock = tankBody.ammunityStock;
            lastFireTime = Time.time;
            Debug.Log($"{gameObject.name}: 사격 성공 - 타겟 {currentTarget.name}");
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            var cont = actionsOut.ContinuousActions;
            cont[0] = Input.GetAxis("Vertical");     // W/S
            cont[1] = Input.GetAxis("Horizontal");   // A/D
            cont[2] = Input.GetKey(KeyCode.Space) ? 1f : 0f; // 사격
            cont[3] = Input.GetKey(KeyCode.Q) ? -1f :
                      Input.GetKey(KeyCode.E) ? 1f : 0f; // 포탑 회전
        }
    }

    private bool CanSeeTarget(GameObject target)
    {
        // 목표까지의 방향을 계산
        Vector2 dirToTarget = (target.transform.position - transform.position).normalized;

        // 시야각 확인
        float angle = Vector2.Angle(turretTransform.up, dirToTarget);
        if (angle < viewAngle / 2f) // 시야각 범위 내일 경우에만 처리
        {
            // Raycast가 목표를 정확히 감지하도록 설정
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, viewDistance, obstacleLayer | enemyLayer | coverLayer);

            // Raycast가 목표와 일치하는지 확인
            if (hit.collider != null && hit.collider.gameObject == target)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Projectile과 충돌한 경우
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // 충돌한 projectile의 owner가 자신이 아닐 경우
            if (collision.gameObject.GetComponent<Projectile>().owner != this)
            {
                AddReward(-2f); // 피격 시 패널티
                Debug.Log($"{gameObject.name}: 피격! - 발사자: {collision.gameObject.GetComponent<Projectile>().owner.gameObject.name}");
            }
        }
    }
    void Update()
    {
        RequestDecision();
    }
}
