using UnityEngine;

public class TankTurret : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool useSmoothRotation = true;
    [SerializeField] private float smoothRotationSpeed = 5f; // �ε巯�� ȸ���� ���� �߰� ����

    private float currentRotationInput = 0f;

    // �ܺ�(TankInputController)���� ȣ��� ȸ�� �޼���
    public void HandleRotation(float input)
    {
        // �Է� ���� ���� (0�̾ ����)
        currentRotationInput = input;
    }

    private void Update()
    {
        // �Է��� ������ ȸ������ ����
        if (currentRotationInput == 0) return;

        if (useSmoothRotation)
        {
            // �ε巯�� ȸ�� (Slerp ���)
            float targetAngle = transform.eulerAngles.z - currentRotationInput * rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                smoothRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // �ﰢ���� ȸ��
            transform.Rotate(0, 0, -currentRotationInput * rotateSpeed * Time.deltaTime);
        }
    }
}