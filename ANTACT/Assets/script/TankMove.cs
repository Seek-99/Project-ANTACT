using UnityEngine;
using UnityEngine.InputSystem;

public class TankMove : MonoBehaviour
{
    new Rigidbody2D rigidbody2D; // new 키워드 추가

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
            rigidbody2D.linearVelocity = input; // 입력 벡터를 그대로 적용
        }
        else
        {
            rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}