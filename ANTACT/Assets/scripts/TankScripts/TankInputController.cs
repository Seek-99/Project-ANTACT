using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputController : MonoBehaviour
{
    [Header("Parts Reference")]
    [SerializeField] private TankBody body;
    [SerializeField] private TankTurret turret;
    [SerializeField] private TankSoundController soundController; // ȿ���� ���� ����
    private Vector2 lastMoveInput = Vector2.zero; // ���� ȿ���� ����

    [Header("Input Sensitivity")]
    [SerializeField] private float moveSensitivity = 1f;
    [SerializeField] private float turretSensitivity = 1f;

    // WASD �� �ٵ� �̵�
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>() * moveSensitivity;
        body.HandleMovement(moveInput);

        // �̵� �Է� �� ȿ����
        if (moveInput.magnitude > 0.1f && lastMoveInput.magnitude <= 0.1f)
        {
            soundController.StartMoveSound(); // �̵� ��
        }
        else if (moveInput.magnitude <= 0.1f && lastMoveInput.magnitude > 0.1f)
        {
            soundController.StopMoveSound(); // ���� ��
        }

        lastMoveInput = moveInput;
    }

    // QE �� �ͷ� ȸ��
    public void OnRotateTurret(InputValue value)
    {
        turret.HandleRotation(value.Get<float>() * turretSensitivity);
    }

    // �߻� �Է� �Լ� �߰�
    public void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            turret.Fire();
        }
    }


    // ����Ƽ �����Ϳ��� �ڵ� ����
    private void Reset()
    {
        body = GetComponentInChildren<TankBody>();
        turret = GetComponentInChildren<TankTurret>();
    }
    // TankInputController.cs�� �ӽ÷� �߰�
    void Update()
    {
        if (Keyboard.current.qKey.isPressed || Keyboard.current.eKey.isPressed)
        {
            Debug.Log("QE Ű �Է� ���� ��");
        }

        // Fire �׼� Ű ����
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            turret.Fire();
        }
    }
}