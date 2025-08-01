using UnityEngine;
using UnityEngine.InputSystem;


public class AmmunityStock : MonoBehaviour
{
    [Header("Ammunition Count")]
    [SerializeField] public float AP = 10f; //철갑탄 개수
    [SerializeField] public float HE = 1f; //고폭탄 개수

    [Header("Current Ammo")]
    [SerializeField] public string status = "ap"; //현재 장전된 탄의 종류

    [Header("Fire Cooldown")]
    [SerializeField] private float fireCooldown = 0.5f; //재장전 시간

    private float lastFireTime = 0f; //마찬가지 재장전시간 관련 변수(TankTurret.cs참고)

    [Header("Ammo MinMax")]
    [SerializeField] public float APmax = 20f; //최대 보유 가능한 포탄량
    [SerializeField] public float HEmax = 2f;

    [Header("Damage Multiplier")]
    [SerializeField] public float Multiple = 1.0f; //데미지 배율

    [Header("isContrllable")]
    [SerializeField] public bool isControllablePlayer = false; //플레이어 전차인지의 여부

    void Update()
    {
        //isControllablePlayer값이 ture일 경우에만 실행할 것이다
        if (!isControllablePlayer) return;

        //1키 = ap, 2키 = he 상태로 지정
        if (Keyboard.current.digit1Key.wasPressedThisFrame || Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            status = "ap";
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame || Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            status = "he";
        }

        // 스페이스 바 눌렀을 시 '현재 장전중인 포탄'이 -1 되게 만드는 코드
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (Time.time - lastFireTime >= fireCooldown) //재장전시간 전에 스페이스 눌렀을 때의 --방지
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
    public float GetCurrentAP() => AP;
    public float GetCurrentHE() => HE;
}
