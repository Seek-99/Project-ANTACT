using UnityEngine;

public class TankTurret : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool useSmoothRotation = true;

    // �ܺ�(TankInputController)���� ȣ��� ȸ�� �޼���
    public void HandleRotation(float input)
    {
        if (input == 0) return;

        if (useSmoothRotation)
        {
            // �ε巯�� ȸ�� (Lerp)
            float targetAngle = transform.eulerAngles.z - input * rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                0.2f
            );
        }
        else
        {
            // �ﰢ���� ȸ��
            transform.Rotate(0, 0, -input * rotateSpeed * Time.deltaTime);
        }
    }
}