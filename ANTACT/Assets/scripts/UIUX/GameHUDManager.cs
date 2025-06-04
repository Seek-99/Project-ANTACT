using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHUDManager : MonoBehaviour
{
    [Header("Tank Count UI")]
    public TMP_Text allyCountText;
    public TMP_Text enemyCountText;

    [Header("Capture Slider")]
    public Slider captureSlider;

    [Header("Round Score")]
    public Image[] roundIcons; // Round1~3 Icon
    public Sprite winSprite;
    public Sprite loseSprite;
    public Sprite noneSprite;

    [Header("Option Menu")]
    public GameObject optionMenuContainer;
    public GameObject popoutContainer;

    [Header("Sound Panel")]
    public GameObject soundPanel;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private float bgmVolume = 0.5f;
    private float sfxVolume = 0.5f;

    private bool isPaused = false;

    void Start()
    {
        // sound
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;

        if (bgmSource != null) bgmSource.volume = bgmVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;

        bgmSlider.onValueChanged.AddListener(OnBGMSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);

        optionMenuContainer.SetActive(false);
        soundPanel.SetActive(false);
    }

    void Update()
    {
        // �ǽð� ���� �� ������Ʈ
        allyCountText.text = $"Allies: {GameObject.FindGameObjectsWithTag("Player").Length}";
        enemyCountText.text = $"Enemies: {GameObject.FindGameObjectsWithTag("Enemy").Length/2}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        optionMenuContainer.SetActive(isPaused);
        soundPanel.SetActive(false);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // ���� �����̴� �� �ݿ�
    public void OnBGMSliderChanged(float value)
    {
        bgmVolume = value;
        if (bgmSource != null)
            bgmSource.volume = value;
    }

    public void OnSFXSliderChanged(float value)
    {
        sfxVolume = value;
        if (sfxSource != null)
            sfxSource.volume = value;
    }

    // ��ư ���� �Լ�
    public void OnClickSounds()
    {
        optionMenuContainer.SetActive(false);
        popoutContainer.SetActive(true);
        soundPanel.SetActive(true);
    }

    public void OnClickBackFromSound()
    {
        soundPanel.SetActive(false);
        popoutContainer.SetActive(false);
        optionMenuContainer.SetActive(true);
    }

    public void OnClickResume()
    {
        isPaused = false;
        optionMenuContainer.SetActive(false);
        soundPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickQuitGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    // ���ɷ�/���� ������Ʈ �� ���
    public void UpdateCaptureGauge(float percent)
    {
        captureSlider.value = percent;
    }

    public void UpdateRoundResult(int roundIndex, bool win)
    {
        if (roundIndex < 0 || roundIndex >= roundIcons.Length) return;
        roundIcons[roundIndex].sprite = win ? winSprite : loseSprite;
    }
}
