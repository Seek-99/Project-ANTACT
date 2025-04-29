using UnityEngine;

public class TankSoundController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource seSource;
    public AudioClip moveClip;
    public AudioClip fireClip;
    public AudioClip hitClip;

    private bool isPlayingMoveSound = false;

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
