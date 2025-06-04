using UnityEngine;
using UnityEngine.UI;

public class HoverHealthUI : MonoBehaviour
{
    public GameObject healthUIPrefab;
    public Vector3 uiOffset = new Vector3(0f, 1.0f, 0f);

    private GameObject uiInstance;
    private Camera mainCamera;
    private Collider2D col;
    private PlayerHealth playerHealth;

    void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        playerHealth = GetComponent<PlayerHealth>();

        // 🟡 프리팹 생성
        uiInstance = Instantiate(healthUIPrefab);
        uiInstance.SetActive(false); // 마우스 올릴 때만 보이도록 기본 숨김

        // 🟡 프리팹의 추적 스크립트에 target과 health 설정
        var follower = uiInstance.GetComponent<HealthUIFollower>();
        if (follower != null)
        {
            follower.target = transform; // 자신
            follower.playerHealth = playerHealth;
            follower.offset = uiOffset;
        }
    }

    void Update()
    {
        if (uiInstance == null || col == null) return;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (col.OverlapPoint(mousePos))
        {
            uiInstance.SetActive(true);
        }
        else
        {
            uiInstance.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (uiInstance != null)
        {
            Destroy(uiInstance);
        }
    }
}
