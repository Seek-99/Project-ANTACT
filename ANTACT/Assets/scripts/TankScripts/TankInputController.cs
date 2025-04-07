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

    // WASD �� �ٵ� �̵�
    public void OnMove(InputValue value)
    {
        body.HandleMovement(value.Get<Vector2>() * moveSensitivity);
    }

    // QE �� �ͷ� ȸ��
    public void OnRotateTurret(InputValue value)
    {
        turret.HandleRotation(value.Get<float>() * turretSensitivity);
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
    }
}