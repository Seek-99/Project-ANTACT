using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [Header("FOV 설정")]
    [Range(0, 360)] public float fov = 90f;
    public int rayCount = 90;
    public float viewDistance = 10f;
    public LayerMask layerMask;

    [Header("탱크 참조")]
    public Transform origin; // 탱크의 Transform (보통 Body_() 또는 Tank)
    public Rigidbody2D tankRb; // 탱크의 Rigidbody2D (회전값용)

    [Header("방향 오프셋")]
    [Tooltip("탱크의 앞 방향 기준 보정값. 위(Y+)가 앞이면 90, 오른쪽(X+)이면 0")]
    public float angleOffset = 90f;

    private Mesh mesh;

    private void Start()
    {
        mesh = new Mesh();
        mesh.name = "FOV";
        GetComponent<MeshFilter>().mesh = mesh;

     
    }

    private void LateUpdate()
    {
        // 탱크 위치 따라가기
        if (origin != null)
        {
            transform.position = origin.position;
        }

        DrawFOV();
    }

    private void DrawFOV()
    {
        float angle = fov * 0.5f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2]; // 0번 중심 + 각도별 점들
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;

        // 현재 회전값 + 오프셋
        float startingAngle = tankRb.rotation + angleOffset;

        for (int i = 0; i <= rayCount; i++)
        {
            float currentAngle = startingAngle - angle; // 시계방향으로 그리기 위해 -
            Vector3 dir = GetVectorFromAngle(currentAngle);

            Vector3 rayOrigin = origin.position;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, dir, viewDistance, layerMask);

            Vector3 hitPoint = hit.collider == null
                ? rayOrigin + dir * viewDistance
                : hit.point;

            // 꼭짓점 좌표는 로컬 기준으로 변환
            vertices[vertexIndex] = transform.InverseTransformPoint(hitPoint);

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // 각도(도) → 방향 벡터
    private Vector3 GetVectorFromAngle(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
