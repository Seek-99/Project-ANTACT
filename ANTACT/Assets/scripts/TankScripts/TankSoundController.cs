using UnityEngine;

public class TankSoundController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource seSource;
    public AudioClip moveClip;
    public AudioClip fireClip;
    public AudioClip hitClip;

    private bool isPlayingMoveSound = false;

    private void Start()
    {
        // 거리 기반 효과 적용: 2D → 3D
        if (seSource != null)
        {
            seSource.spatialBlend = 1f; 
            seSource.minDistance = 5f;  
            seSource.maxDistance = 60f; 
            seSource.rolloffMode = AudioRolloffMode.Logarithmic; 
        }
    }

    public void StartMoveSound()
    {
        if (!isPlayingMoveSound && moveClip != null)
        {
            seSource.clip = moveClip;
            seSource.loop = true;
            seSource.Play();
            isPlayingMoveSound = true;
        }
    }

    public void StopMoveSound()
    {
        if (isPlayingMoveSound)
        {
            seSource.Stop();
            seSource.loop = false;
            seSource.clip = null;
            isPlayingMoveSound = false;
        }
    }

    public void PlayFireSound()
    {
        if (fireClip != null && seSource != null)
            seSource.PlayOneShot(fireClip);
    }

    public void PlayHitSound()
    {
        if (hitClip != null && seSource != null)
            seSource.PlayOneShot(hitClip);
    }
}
