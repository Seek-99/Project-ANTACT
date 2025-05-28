using UnityEngine;

public class TankBody : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float rotationDragFactor = 0.5f; // 회전 시 이동 속도 감소 비율 (0~1)

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    public AmmunityStock ammunityStock;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        if (ammunityStock == null)
        {
            ammunityStock = GetComponent<AmmunityStock>();
            if (ammunityStock == null)
            {
                Debug.LogWarning($"{gameObject.name}: AmmunityStock을 찾을 수 없습니다.");
            }
        }
    }

    // 외부(TankInputController)에서 호출될 이동 메서드
    public void HandleMovement(Vector2 input)
    {
        // 회전 입력이 있으면 이동 속도 감소 (탱크 스티어링)
        float speedMultiplier = Mathf.Abs(input.x) > 0.1f ? rotationDragFactor : 1f;

        // 전진/후진 (W/S) - 회전 중일 때는 속도 제한
        rb.linearVelocity = transform.up * (input.y * moveSpeed * speedMultiplier);

        // 좌우 회전 (A/D)
        rb.angularVelocity = -input.x * rotateSpeed;
    }

    // 물리 설정 초기화 (필요 시)
    public void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = false;
    }

    // 현재 속도 반환 (Agent에서 관측용)
    public Vector2 GetVelocity()
    {
        return rb.linearVelocity;
    }

    // ML-Agent용: 전진/후진 제어
    public void Move(float moveInput)
    {
        rb.linearVelocity = transform.up * moveInput * moveSpeed;
    }

    // ML-Agent용: 회전 제어
    public void Turn(float turnInput)
    {
        rb.angularVelocity = -turnInput * rotateSpeed;
    }
}