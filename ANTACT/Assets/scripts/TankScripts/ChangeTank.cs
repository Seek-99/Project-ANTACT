using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents.Policies;
using UnityEngine.AI;

public class ChangeTank : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

    private void LateUpdate()
    {
        if (player1 == null || player2 == null || player3 == null) return;

        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            SetToAI(player2);
            SetToAI(player3);
            SetToPlayer(player1);
        }

        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            SetToAI(player1);
            SetToAI(player3);
            SetToPlayer(player2);
        }

        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            SetToAI(player1);
            SetToAI(player2);
            SetToPlayer(player3);

        }


    }

    private void SetToAI(GameObject tank)
    {
        tank.GetComponent<TankInputController>().enabled = false;
        tank.GetComponent<PlayerInput>().enabled = false;
        tank.GetComponent<TankFSMController>().enabled = true;
        tank.GetComponent<NavMeshAgent>().enabled = true;
        tank.GetComponent<TankAgent>().enabled = true;

        Transform turret = tank.transform.Find("body_0/Turret_0");
        if (turret != null)
        {
            turret.tag = "Untagged"; // AI 모드일 때 터렛 태그 변경
        }

        var ammo = tank.GetComponentInChildren<AmmunityStock>();
        if (ammo != null)
        {
            ammo.isControllablePlayer = false;
            ammo.status = "ap";
        }

        Transform fov = tank.transform.Find("FieldOfView");
        if (fov != null)
            fov.gameObject.SetActive(false);
    }

    private void SetToPlayer(GameObject tank)
    {
        tank.GetComponent<TankInputController>().enabled = true;
        tank.GetComponent<PlayerInput>().enabled = true;
        tank.GetComponent<TankFSMController>().enabled = false;
        tank.GetComponent<NavMeshAgent>().enabled = false;
        tank.GetComponent<TankAgent>().enabled = false;

        Transform turret = tank.transform.Find("body_0/Turret_0");
        if (turret != null)
        {
            turret.tag = "player"; // AI 모드일 때 터렛 태그 변경
        }

        var ammo = tank.GetComponentInChildren<AmmunityStock>();
        if (ammo != null)
        {
            ammo.isControllablePlayer = true;
            ammo.status = "ap";
        }

        Transform fov = tank.transform.Find("FieldOfView");
        if (fov != null)
            fov.gameObject.SetActive(true);
    }
}
