using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupporterSpawner : MonoBehaviour
{
    [SerializeField]
    Transform HQ; // 소환 위치
    public float spawnsecond = 20f;
    public GameObject supplyVehiclePrefab;

    private void Start()
    {
        StartCoroutine(SupplySpawnLoop());
    }

    private IEnumerator SupplySpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnsecond);
            Instantiate(supplyVehiclePrefab, HQ.position, Quaternion.identity);
            Debug.Log("✅ 보급 차량 생성됨!");
        }
    }

}
