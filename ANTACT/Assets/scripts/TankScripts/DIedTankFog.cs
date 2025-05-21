using UnityEngine;
using System.Collections;

public class DiedTankFog : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float delaySeconds = 1f;

    void Start()
    {
        StartCoroutine(LoopParticle());
    }

    IEnumerator LoopParticle()
    {
        while (true)
        {
            particleSystem.Play();

            // 파티클 재생 시간만큼 대기
            yield return new WaitForSeconds(particleSystem.main.duration);

            particleSystem.Stop();

            // 1초 텀
            yield return new WaitForSeconds(delaySeconds);
        }
    }
}
