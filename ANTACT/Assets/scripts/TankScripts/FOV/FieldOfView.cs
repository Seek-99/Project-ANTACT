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
    public Transform origin; // 위치 기준
    public Transform rotationSource; // 🎯 회전값을 참조할 오브젝트 (예: 바디)

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
        if (origin != null)
        {
            transform.position = origin.position;
        }

        DrawFOV();
        UpdateEnemyVisibility();
    }

    private void DrawFOV()
    {
        float angle = fov * 0.5f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;

        float startingAngle = GetRotationZ() + angleOffset;

        for (int i = 0; i <= rayCount; i++)
        {
            float currentAngle = startingAngle - angle;
            Vector3 dir = GetVectorFromAngle(currentAngle);

            Vector3 rayOrigin = origin.position;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, dir, viewDistance, layerMask);

            Vector3 hitPoint = hit.collider == null
                ? rayOrigin + dir * viewDistance
                : hit.point;

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

    private void UpdateEnemyVisibility()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 dirToEnemy = (enemy.transform.position - origin.position).normalized;
            float distanceToEnemy = Vector3.Distance(origin.position, enemy.transform.position);

            if (distanceToEnemy > viewDistance)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = false;
                continue;
            }

            float startingAngle = GetRotationZ() + angleOffset;
            Vector3 forward = GetVectorFromAngle(startingAngle);

            float angleToEnemy = Vector3.Angle(forward, dirToEnemy);

            if (angleToEnemy > fov * 0.5f)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = false;
                continue;
            }

            RaycastHit2D hit = Physics2D.Raycast(origin.position, dirToEnemy, distanceToEnemy, layerMask);

            bool isVisible = hit.collider == null || hit.collider.gameObject == enemy;
            enemy.GetComponent<SpriteRenderer>().enabled = isVisible;
        }
    }

    private float GetRotationZ()
    {
        return rotationSource != null ? rotationSource.eulerAngles.z : 0f;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
