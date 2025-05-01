using UnityEngine;

public class AmmoSupply : MonoBehaviour
{
    [Header("Ammunition Supply")]
    [SerializeField] private int APsup = 10; //º¸ÃæÇÒ Ã¶°©Åº °³¼ö
    [SerializeField] private int HEsup = 10; //º¸ÃæÇÒ °íÆøÅº °³¼ö
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AmmunityStock Ammo = collision.GetComponent<AmmunityStock>();
        if (Ammo != null)
        {
            if (Ammo.AP + APsup < Ammo.APmax) //Ã¶°©Åº º¸±Þ ¸ÞÄ¿´ÏÁò
            {
                Ammo.AP += APsup;
            }
            else
            {
                Ammo.AP = Ammo.APmax;
            }

            if (Ammo.HE + HEsup < Ammo.HEmax) //°íÆøÅº º¸±Þ ¸ÞÄ¿´ÏÁò
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
            Debug.LogWarning("AmmunityStock ÄÄÆ÷³ÍÆ®¸¦ Ã£À» ¼ö ¾ø½À´Ï´Ù.");
        }
    }
}
