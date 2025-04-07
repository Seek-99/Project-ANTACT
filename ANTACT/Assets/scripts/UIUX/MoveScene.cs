using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    // 인스펙터에서 선택할 씬 이름
    [SerializeField] 
    private string targetSceneName;
    
    // 버튼 클릭 시 호출될 메서드
    public void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }
}