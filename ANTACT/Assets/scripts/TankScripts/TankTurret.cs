using UnityEngine;
using Unity.MLAgents;

public class TankTurret : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool useSmoothRotation = true;
    [SerializeField] private float smoothRotationSpeed = 5f; // �ε巯�� ȸ���� ���� �߰� ����

    private float currentRotationInput = 0f;

    public void HandleRotation(float input)
    {
        currentRotationInput = input;
    }

    private void Update()
    {
        if (currentRotationInput == 0) return;

        if (useSmoothRotation)
        {
            float targetAngle = transform.eulerAngles.z - currentRotationInput * rotateSpeed;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0, 0, targetAngle),
                smoothRotationSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.Rotate(0, 0, -currentRotationInput * rotateSpeed * Time.deltaTime);
        }
    }

    [Header("Fire Settings")]

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private TankSoundController soundController; // 효과음 동작 연결

    private float lastFireTime = 0f;

    public void Fire(Agent agentowner)
    {
        if (Time.time - lastFireTime < fireCooldown) return;

        lastFireTime = Time.time;
        // 발사체 생성 및 owner 설정
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.owner = agentowner;
        }


        // 효과음 재생
        if (soundController != null)
        {
            soundController.PlayFireSound();
        }
    }
}