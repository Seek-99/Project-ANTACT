using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 5.0f;
    public Vector3 offset; // 카메라와 플레이어 사이의 고정 거리
    public GameObject player;

    private void LateUpdate()
    {
        if (player == null) return;

        // 원하는 카메라 위치 계산 (플레이어 위치 + 오프셋)
        Vector3 desiredPosition = player.transform.position + offset;
        // 부드러운 이동 적용
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // 2D 게임인 경우 Z 좌표 유지
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}