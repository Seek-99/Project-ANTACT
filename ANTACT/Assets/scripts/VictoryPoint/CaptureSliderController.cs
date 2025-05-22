using UnityEngine;
using UnityEngine.UI;

public class CaptureSliderController : MonoBehaviour
{
    private Slider slider;
    private Image fillImage;

    public VictoryCircle victorycircle;

    private Color greenColor = new Color32(0x30, 0xC1, 0x2F, 0xFF); // 초록
    private Color redColor = new Color32(0xBA, 0x40, 0x28, 0xFF);  // 빨강

    void Start()
    {
        slider = GetComponent<Slider>();
        fillImage = transform.Find("Fill Area/Fill").GetComponent<Image>();
    }

    void Update()
    {
        if (victorycircle == null || slider == null || fillImage == null) return;

        float point = victorycircle.VictoryPoint;
        slider.value = Mathf.Clamp(Mathf.Abs(point), 0, 100);

        if (point > 0)
            fillImage.color = greenColor;
        else if (point < 0)
            fillImage.color = redColor;
    }
}