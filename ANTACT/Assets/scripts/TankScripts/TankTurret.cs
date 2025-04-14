using UnityEngine;

public class TankTurret : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool useSmoothRotation = true;
    [SerializeField] private float smoothRotationSpeed = 5f; // 부드러운 회전을 위한 추가 변수

    private float currentRotationInput = 0f;

    // 외부(TankInputController)에서 호출될 회전 메서드
    public void HandleRotation(float input)
    {
        // 입력 값을 저장 (0이어도 저장)
        currentRotationInput = input;
    }

    private void Update()
    {
        // 입력이 없으면 회전하지 않음
        if (currentRotationInput == 0) return;

        if (useSmoothRotation)
        {
            // 부드러운 회전 (Slerp 사용)
            float targetAngle = transform.eulerAngles.z - currentRotationInput * rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                smoothRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            // 즉각적인 회전
            transform.Rotate(0, 0, -currentRotationInput * rotateSpeed * Time.deltaTime);
        }
    }

    //-----------발사 세팅-----------
    [Header("Fire Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.5f;

    private float lastFireTime = 0f;

    public void Fire()
    {
        if (Time.time - lastFireTime < fireCooldown) return;

        lastFireTime = Time.time;
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}