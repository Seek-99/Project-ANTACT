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


    [Header("BGM Audio")]
    public AudioSource bgmSource;
    public Slider volumeSlider;
    public TMP_Text volumeValueText;

    [Header("SFX Audio")]
    public AudioSource sfxSource;
    public Slider sfxVolumeSlider;
    public TMP_Text sfxVolumeValueText;

    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

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
        SceneManager.LoadScene("Map1Scene");
    }


    // -------------사운드 패널--------------------
    public void OnClickSoundSettings()
    {
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(true);

        // 슬라이더 초기값 반영
        volumeSlider.value = bgmSource.volume;
        UpdateVolumeText(bgmSource.volume);

        sfxVolumeSlider.value = sfxSource.volume;
        UpdateSFXVolumeText(sfxSource.volume);
    }

    private void Start()
    {
        // 초기 볼륨 설정
        volumeSlider.value = bgmVolume;
        sfxVolumeSlider.value = sfxVolume;

        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;

        UpdateVolumeText(bgmVolume);
        UpdateSFXVolumeText(sfxVolume);

        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)
    {
        bgmVolume = value;
        if (bgmSource != null)
            bgmSource.volume = value;

        UpdateVolumeText(value);
    }

    public void OnSFXSliderValueChanged(float value) // 추가
    {
        sfxVolume = value;
        if (sfxSource != null)
            sfxSource.volume = value;

        UpdateSFXVolumeText(value);
    }

    private void UpdateVolumeText(float value)
    {
        volumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    private void UpdateSFXVolumeText(float value) // 추가
    {
        sfxVolumeValueText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnClickApplySoundSettings()
    {
        bgmSource.volume = volumeSlider.value;
        sfxSource.volume = sfxVolumeSlider.value;

        Debug.Log($"[사운드 적용] BGM: {bgmSource.volume}, SFX: {sfxSource.volume}");
    }

    public void OnClickBackFromSound()
    {
        soundPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(true);
    }
}
