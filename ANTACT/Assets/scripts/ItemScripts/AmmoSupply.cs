using UnityEngine;

public class AmmoSupply : MonoBehaviour
{
    [Header("Ammunition Supply")]
    [SerializeField] private int APsup = 10; //������ ö��ź ����
    [SerializeField] private int HEsup = 10; //������ ����ź ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AmmunityStock Ammo = collision.GetComponent<AmmunityStock>();
        if (Ammo != null)
        {
            if (Ammo.AP + APsup < Ammo.APmax) //ö��ź ���� ��Ŀ����
            {
                Ammo.AP += APsup;
            }
            else
            {
                Ammo.AP = Ammo.APmax;
            }

            if (Ammo.HE + HEsup < Ammo.HEmax) //����ź ���� ��Ŀ����
            {
                Ammo.HE += HEsup;
            }
            else
            {
                Ammo.HE = Ammo.HEmax;
            }
        }
        else
        {
            Debug.LogWarning("AmmunityStock ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
