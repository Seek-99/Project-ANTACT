using UnityEngine;
using UnityEngine.InputSystem;

public class AmmunityStock : MonoBehaviour
{
    [Header("Ammunition Count")] //public�� ������� �ʰ� ������ �����ϰ� �ؾ������� -> ������������ �����ؾ� �ϴ� ������.
    [SerializeField] public int AP = 10; //ö��ź ����
    [SerializeField] public int HE = 10; //����ź ����

    [Header("Current Ammo")]
    [SerializeField] private string status = "ap"; //���� ������ ź�� ����

    [Header("Fire Cooldown")]
    [SerializeField] private float fireCooldown = 0.5f; //������ �ð�

    private float lastFireTime = 0f; //�������� �������ð� ���� ����(TankTurret.cs����)

    [Header("Ammo MinMax")]
    [SerializeField] public int APmax = 20; //�ִ� ���� ������ ��ź�� if(AP < MAX) �̷� �������� ����� ����. ���������� public �ذ��� �� ������ �ذ�����.
    [SerializeField] public int HEmax = 10;

    [Header("Damage Multiplier")]
    [SerializeField] private int Multiple = 1; //������ ���� �������
    
    GameObject obj; //������� ��ŸƮ�κб��� ���߿� �� ���ƾ���� �� �� -> public�� ����ϸ� ������ �ټ� �߰��Ǿ��� �� �ʿ������� critical error �߻��Ұ���.
    private void Start()
    {
        obj = GameObject.Find("Projectile");
        obj.GetComponent<Projectile>().damage = Multiple * 30f; //������ƿ ��ũ��Ʈ���� ���� public�� ������ �����ͼ� ������ ���� ���ϱ�
    }
    //������� ���ƾ���� ��. ������ �����Ұ�.


    void Update()
    {
        //1Ű = ap, 2Ű = he ���·� ����
        if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            status = "ap";
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            status = "he";
        }

        // �����̽� �� ������ �� '���� �������� ��ź'�� -1 �ǰ� ����� �ڵ�
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (Time.time - lastFireTime >= fireCooldown) //�������ð� ���� �����̽� ������ ���� --����
            {
                if (status == "ap" && AP > 0)
                {
                    AP--;
                    lastFireTime = Time.time;
                }
                else if (status == "he" && HE > 0)
                {
                    HE--;
                    lastFireTime = Time.time;
                }
            }
            else return;
        }
    }
    public int GetCurrentAP() => AP;
}
