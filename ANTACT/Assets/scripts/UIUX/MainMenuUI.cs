using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    // ��ü �����̳�
    public GameObject mainMenuContainer;
    public GameObject popoutDialogContainer;

    // ���� �г�
    public GameObject gameStartPanel_Dialog;
    public GameObject optionsPanel_Dialog;
    public GameObject soundPanel_Dialog;


    // ���� ���� UI
    public Slider volumeSlider;
    public TMP_Text volumeValueText;

    private float currentVolume = 0.5f;


    //---------------��ư �̺�Ʈ------------------------
    // ���� ���� ��ư
    public void OnClickGameStart()
    {
        mainMenuContainer.SetActive(false);
        popoutDialogContainer.SetActive(true);
        gameStartPanel_Dialog.SetActive(true);
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(false);
    }

    // �ɼ� ��ư
    public void OnClickOptions()
    {
        mainMenuContainer.SetActive(false);
        popoutDialogContainer.SetActive(true);
        gameStartPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(true);
        soundPanel_Dialog.SetActive(false);
    }

    // ���� ��ư
    public void OnClickExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Dialog �ڷΰ��� ��ư
    public void OnClickBackToMain()
    {
        gameStartPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(false);
        popoutDialogContainer.SetActive(false);
        mainMenuContainer.SetActive(true);
    }

    // �ó����� ����
    public void OnClickScenario1()
    {
        SceneManager.LoadScene("GameScene");
    }


    // -------------���� �г�--------------------
    public void OnClickSoundSettings()
    {
        optionsPanel_Dialog.SetActive(false);
        soundPanel_Dialog.SetActive(true);

        // �����̴� �ʱⰪ ����
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
        AudioListener.volume = currentVolume; // ��ü ���� ����
        Debug.Log("���� ����: " + currentVolume);
    }

    public void OnClickBackFromSound()
    {
        soundPanel_Dialog.SetActive(false);
        optionsPanel_Dialog.SetActive(true);
    }
}
