using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private bool isPaused = false;

    void Update()
    {
        allyCountText.text = $"Allies: {GameObject.FindGameObjectsWithTag("Player").Length}";
        enemyCountText.text = $"Enemies: {GameObject.FindGameObjectsWithTag("Enemy").Length}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        optionMenuContainer.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void UpdateCaptureGauge(float percent)
    {
        captureSlider.value = percent;
    }

    public void UpdateRoundResult(int roundIndex, bool win)
    {
        if (roundIndex < 0 || roundIndex >= roundIcons.Length) return;
        roundIcons[roundIndex].sprite = win ? winSprite : loseSprite;
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnClickResume()
    {
        isPaused = false;
        optionMenuContainer.SetActive(false);
        Time.timeScale = 1f;
    }
}
