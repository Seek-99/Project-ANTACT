using UnityEngine;
using UnityEngine.AI;

public class navigation : MonoBehaviour
{
    [SerializeField]
    Transform target; // ��ǥ Transform
    [SerializeField]
    Transform HQ; // ��ǥ Transform2
    private int previousdestination = 0;
    public float diversionDistance = 10f;
    [SerializeField]
    NavMeshAgent navMeshAgent; // NavMeshAgent ������Ʈ

    void Start()
    {
        // NavMeshAgent ������Ʈ �ʱ�ȭ
        navMeshAgent = GetComponent<NavMeshAgent>();

        // NavMeshAgent�� ȸ�� ���� ���� 2d ȯ���̶� z���� ���ִ� �ڵ�
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void Update()
    {
        //������ �������� ������ �Ÿ�
        

        // NavMeshAgent�� ������ ����
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
        // �̵� ������ �ٶ󺸰� �ϱ�
        LookAtMovementDirection();
    }

    void LookAtMovementDirection()
    {
        // NavMeshAgent�� �ӵ��� �������� ���� ���
        Vector3 velocity = navMeshAgent.velocity;

        // �ӵ��� ���� ���� ȸ�� ó��
        if (velocity.sqrMagnitude > 0.01f) // �ӵ��� ���� 0�� �ƴϸ�
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
        }
    }
}
