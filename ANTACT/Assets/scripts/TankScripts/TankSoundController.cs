using UnityEngine;

public class TankSoundController : MonoBehaviour
{
    public AudioSource seSource;
    public AudioClip moveClip;
    public AudioClip fireClip;

    public void PlayMoveSound()
    {
        if (moveClip != null)
            seSource.PlayOneShot(moveClip);
    }

    public void PlayFireSound()
    {
        if (fireClip != null)
            seSource.PlayOneShot(fireClip);
    }
}
