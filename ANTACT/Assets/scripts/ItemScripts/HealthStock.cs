using UnityEngine;
using UnityEngine.InputSystem;

public class HealthStock : MonoBehaviour
{
    [SerializeField] public int HealthValue = 0;

    private int HealthAmount = 30;

    GameObject obj;

    void Update()
    {
        if (Keyboard.current.digit4Key.wasPressedThisFrame || Keyboard.current.numpad4Key.wasPressedThisFrame)
        {
            if (HealthValue <= 0)
            {
                return;
            }
            else
            {   
                HealthValue -= 1;                
                GetComponent<PlayerHealth>().currentHealth += 30;                    
            }
        }
    }
    public int GetCurrentHealthValue() => HealthValue;
}
