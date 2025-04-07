using UnityEngine;

public class TankBody : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 100f;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // 외부(TankInputController)에서 호출될 이동 메서드
    public void HandleMovement(Vector2 input)
    {
        // 전진/후진 (W/S)
        rb.linearVelocity = transform.up * input.y * moveSpeed;

        // 좌우 회전 (A/D)
        rb.angularVelocity = -input.x * rotateSpeed;
    }

    // 물리 설정 초기화 (필요 시)
    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = false;
    }
}