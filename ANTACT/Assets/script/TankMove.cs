using UnityEngine;
using UnityEngine.InputSystem;

public class TankMove : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    public float moveSpeed = 5f;     // ����/���� �ӵ�
    public float rotateSpeed = 100f; // ȸ�� �ӵ� (�ʴ� ����)

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        // W/S: ����/���� (input.y)
        float moveDirection = input.y;
        rigidbody2D.linearVelocity = transform.up * moveDirection * moveSpeed;

        // A/D: ȸ�� (input.x)
        float rotateDirection = -input.x; // (-)�� �ݴ� ���� ����
        rigidbody2D.angularVelocity = rotateDirection * rotateSpeed;
    }
}