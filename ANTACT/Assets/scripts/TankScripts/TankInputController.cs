using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputController : MonoBehaviour
{
    [Header("Parts Reference")]
    [SerializeField] private TankBody body;
    [SerializeField] private TankTurret turret;
    [SerializeField] private TankSoundController soundController; // 효과음 동작 연결
    private Vector2 lastMoveInput = Vector2.zero; // 엔진 효과음 루프

    [Header("Input Sensitivity")]
    [SerializeField] private float moveSensitivity = 1f;
    [SerializeField] private float turretSensitivity = 1f;

    // WASD → 바디 이동
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>() * moveSensitivity;
        body.HandleMovement(moveInput);

        // 이동 입력 시 효과음
        if (moveInput.magnitude > 0.1f && lastMoveInput.magnitude <= 0.1f)
        {
            soundController.StartMoveSound(); // 이동 시
        }
        else if (moveInput.magnitude <= 0.1f && lastMoveInput.magnitude > 0.1f)
        {
            soundController.StopMoveSound(); // 멈춤 시
        }

        lastMoveInput = moveInput;
    }

    // QE → 터렛 회전
    public void OnRotateTurret(InputValue value)
    {
        turret.HandleRotation(value.Get<float>() * turretSensitivity);
    }

    // 발사 입력 함수 추가
    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            turret.Fire(GetComponent<TankAgent>());
        }
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

        // Fire 액션 키 매핑
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            turret.Fire(GetComponent<TankAgent>());
        }
    }
}