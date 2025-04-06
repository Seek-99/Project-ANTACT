using UnityEngine;
using UnityEngine.InputSystem;

public class TankMove : MonoBehaviour
{
    new Rigidbody2D rigidbody2D; // new Ű���� �߰�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        //Debug.Log("x: " + input.x + ", y: " + input.y);
        if (input.magnitude > 0)
        {
            rigidbody2D.linearVelocity = input; // �Է� ���͸� �״�� ����
        }
        else
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}