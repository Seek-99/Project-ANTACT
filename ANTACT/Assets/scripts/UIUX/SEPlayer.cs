using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    public AudioSource seSource;

    void Update()
    {
        // 메인 카메라 위치를 따라가게
        if (Camera.main != null)
        {
            transform.position = Camera.main.transform.position;
        }
    }

    public void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null || seSource == null)
        {
            Debug.LogWarning("SEPlayer: No AudioClip or AudioSource.");
            return;
        }

        seSource.spatialBlend = 1f; // 3D 사운드 적용
        seSource.volume = volume;
        seSource.PlayOneShot(clip);
    }
}
