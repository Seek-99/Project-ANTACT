using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float duration = 0.8f;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}

