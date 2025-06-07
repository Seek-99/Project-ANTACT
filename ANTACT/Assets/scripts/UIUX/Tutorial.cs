using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{


    public GameObject HelperUI;
    public GameObject completeButton;
    public Text HelperText;
    public PlayerHealth playerHealth; // 플레이어의 Health 스크립트 참조
    private int currentStep = 0;


    void Start()
    {
        StartCoroutine(ShowTutorial());

    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SkipStep();
        }
    }


    IEnumerator ShowTutorial()
    {
        ChangeText("튜토리얼을 시작합니다.");
        yield return new WaitForSeconds(3f);
        ChangeText("Tab 키를 누르시오.");
    }

    void SkipStep()
    {
        currentStep++;

        switch (currentStep)
        {
            case 1:
                ChangeText("a,s,w,d로 조작합니다.");
                break;
            case 2:
                ChangeText("회전과 동시에 전후진시 감속됩니다.");
                break;
            case 3:
                ChangeText("화살표를 따라 다음 위치로 이동하세요.");
                break;
            case 4:
                ChangeText("장애물 뒤에 숨어있는 적은 시야에 보이지 않습니다.");
                break;



            case 5:
                ChangeText("파괴 가능 장애물과 파괴 불가 장애물이 있습니다.");
                break;
            case 6:
                ChangeText("숨어있는 적을 파괴하세요.");
                break;
            case 7:
                ChangeText("잘하셨습니다.");
                break;
            case 8:
                ChangeText("f2를 눌러 다른 전차로 이동하세요.");
                break;
            case 9:
                ChangeText("f1,f2..를 이용해 다른 전차로 이동할 수 있습니다.");
                break;
            case 10:
                if (playerHealth != null)
                {
                    playerHealth.currentHealth = 50f;

                    ChangeText("체력이 50으로 줄었습니다.");
                }
                break;
            case 11:
                ChangeText("4번 키를 이용해 체력을 회복하세요.");
                break;
            case 12:
                ChangeText("이제 마지막입니다.");
                break;
            case 13:
                ChangeText("승리를 하는 방법은 적을 전멸시키거나, 거점을 점령하면 됩니다.");
                break;
            case 14:
                ChangeText("원에 들어가 거점을 점령하세요.");
                if (completeButton != null)
                    completeButton.SetActive(true);
                break;
            default:
                HelperUI.SetActive(false);
                break;
        }
    }

    void ChangeText(string text)
    {
        if (HelperText != null)
        {
            HelperText.text = text;
        }
    }

    public void OnClickCompleteTutorial()
    {
        SceneManager.LoadScene("Map1Scene");
    }
}