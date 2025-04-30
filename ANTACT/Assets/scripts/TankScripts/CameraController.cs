using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 5.0f;
    public Vector3 offset; // 카메라와 플레이어 사이의 고정 거리
    public GameObject player;

    [Header("Zoom Settings")]
    public float zoomSpeed = 5f;
    public float minZoom = 3f;
    public float maxZoom = 10f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // 카메라 위치 부드럽게 따라가기
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Z 고정
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        //  마우스 휠 줌 처리
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }
}
