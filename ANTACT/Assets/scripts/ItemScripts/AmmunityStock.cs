using UnityEngine;
using UnityEngine.InputSystem;

public class AmmunityStock : MonoBehaviour
{
    [Header("Ammunition Count")] //public을 사용하지 않고 참조가 가능하게 해야할지도 -> 보급차량에서 참조해야 하는 변수임.
    [SerializeField] public int AP = 10; //철갑탄 개수
    [SerializeField] public int HE = 10; //고폭탄 개수

    [Header("Current Ammo")]
    [SerializeField] private string status = "ap"; //현재 장전된 탄의 종류

    [Header("Fire Cooldown")]
    [SerializeField] private float fireCooldown = 0.5f; //재장전 시간

    private float lastFireTime = 0f; //마찬가지 재장전시간 관련 변수(TankTurret.cs참고)

    [Header("Ammo MinMax")]
    [SerializeField] public int APmax = 20; //최대 보유 가능한 포탄량 if(AP < MAX) 이런 느낌으로 사용할 예정. 마찬가지로 public 해결할 수 있으면 해결하자.
    [SerializeField] public int HEmax = 10;

    [Header("Damage Multiplier")]
    [SerializeField] private int Multiple = 1; //데미지 배율 설정기능
    
    GameObject obj; //여기부터 스타트부분까지 나중에 싹 갈아엎어야 할 듯 -> public을 사용하면 전차가 다수 추가되었을 때 필연적으로 critical error 발생할것임.
    private void Start()
    {
        obj = GameObject.Find("Projectile");
        obj.GetComponent<Projectile>().damage = Multiple * 30f; //프로젝틸 스크립트에서 현재 public인 데미지 가져와서 데미지 배율 곱하기
    }
    //여기까지 갈아엎어야 함. 로직만 참고할것.


    void Update()
    {
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
}
