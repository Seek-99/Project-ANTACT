// DarkenMapCreator.cs
using UnityEngine;

public class DarkenMapCreator : MonoBehaviour
{
    public Camera mainCamera;
    public Material darkenMaterial;

    private void Start()
    {
        GameObject darkenObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        darkenObj.name = "DarknessOverlay";
        Destroy(darkenObj.GetComponent<Collider>());

        darkenObj.transform.parent = mainCamera.transform;
        darkenObj.transform.localPosition = new Vector3(0, 0, 10); // ī�޶� ��
        darkenObj.transform.localRotation = Quaternion.identity;
        darkenObj.transform.localScale = new Vector3(350, 350, 1); // �ʺ��� ũ��

        MeshRenderer renderer = darkenObj.GetComponent<MeshRenderer>();
        renderer.material = darkenMaterial;
    }
}
