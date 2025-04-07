using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputController : MonoBehaviour
{
    [Header("Parts Reference")]
    [SerializeField] private TankBody body;
    [SerializeField] private TankTurret turret;

    [Header("Input Sensitivity")]
    [SerializeField] private float moveSensitivity = 1f;
    [SerializeField] private float turretSensitivity = 1f;

    // WASD → 바디 이동
    public void OnMove(InputValue value)
    {
        body.HandleMovement(value.Get<Vector2>() * moveSensitivity);
    }

    // QE → 터렛 회전
    public void OnRotateTurret(InputValue value)
    {
        turret.HandleRotation(value.Get<float>() * turretSensitivity);
    }

    // 유니티 에디터에서 자동 연결
    private void Reset()
    {
        body = GetComponentInChildren<TankBody>();
        turret = GetComponentInChildren<TankTurret>();
    }
    // TankInputController.cs에 임시로 추가
    void Update()
    {
        if (Keyboard.current.qKey.isPressed || Keyboard.current.eKey.isPressed)
        {
            Debug.Log("QE 키 입력 감지 중");
        }
    }
}