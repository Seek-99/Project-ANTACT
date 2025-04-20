using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
{
    Destroy(gameObject);
    Debug.Log("Destroy");
}
}