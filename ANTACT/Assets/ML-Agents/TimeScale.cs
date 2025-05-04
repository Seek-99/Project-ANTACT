using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    void Start()
    {
        // 훈련 중 배속을 없애기 위해 timeScale을 1로 설정
        Time.timeScale = 1.0f;
    }
}