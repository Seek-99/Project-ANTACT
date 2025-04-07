using UnityEngine;

public class TankTurret : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool useSmoothRotation = true;

    // 외부(TankInputController)에서 호출될 회전 메서드
    public void HandleRotation(float input)
    {
        if (input == 0) return;

        if (useSmoothRotation)
        {
            // 부드러운 회전 (Lerp)
            float targetAngle = transform.eulerAngles.z - input * rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                0.2f
            );
        }
        else
        {
            // 즉각적인 회전
            transform.Rotate(0, 0, -input * rotateSpeed * Time.deltaTime);
        }
    }
}