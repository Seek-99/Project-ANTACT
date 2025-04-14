using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    // 전체 컨테이너
    public GameObject mainMenuContainer;
    public GameObject popoutDialogContainer;

    // 개별 패널
    public GameObject gameStartPanel_Dialog;
    public GameObject optionsPanel_Dialog;
    public GameObject soundPanel_Dialog;


    // 사운드 설정 UI
    public Slider volumeSlider;
    public TMP_Text volumeValueText;

    private float currentVolume = 0.5f;


    //---------------버튼 이벤트------------------------
    // 게임 시작 버튼
    public void OnClickGameStart()
    {
        mainMenuContainer.SetActive(false);
        popoutDialogContainer.SetActive(true);
        gameStartPanel_Dialog.SetActive(true);
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(false);
    }

    // 옵션 버튼
    public void OnClickOptions()
    {
        mainMenuContainer.SetActive(false);
        popoutDialogContainer.SetActive(true);
        gameStartPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(true);
        soundPanel_Dialog.SetActive(false);
    }

    // 종료 버튼
    public void OnClickExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Dialog 뒤로가기 버튼
    public void OnClickBackToMain()
    {
        gameStartPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(false);
        popoutDialogContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

    // 시나리오 선택
    public void OnClickScenario1()
    {
        SceneManager.LoadScene("GameScene");
    }


    // -------------사운드 패널--------------------
    public void OnClickSoundSettings()
    {
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(true);

        // 슬라이더 초기값 적용
        volumeSlider.value = currentVolume;
        UpdateVolumeText(currentVolume);
    }

    public void OnSliderValueChanged(float value)
    {
        UpdateVolumeText(value);
    }

    private void UpdateVolumeText(float value)
    {
        volumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnClickApplySoundSettings()
    {
        currentVolume = volumeSlider.value;
        AudioListener.volume = currentVolume; // 전체 볼륨 적용
        Debug.Log("볼륨 적용: " + currentVolume);
    }

    public void OnClickBackFromSound()
    {
        soundPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(true);
    }
}
