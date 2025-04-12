using UnityEngine;
using UnityEngine.UI;

public class HealthBarTickMarks : MonoBehaviour
{
    public Slider slider;
    public GameObject tickMarkPrefab;
    public int ticksPerInterval = 10; // 10당 1눈금

    void Start()
    {
        slider.interactable = false;
        CreateTickMarks();
    }

    void CreateTickMarks()
    {
        if (tickMarkPrefab == null) return;

        int totalTicks = Mathf.FloorToInt(slider.maxValue / ticksPerInterval);
        float widthPerTick = slider.GetComponent<RectTransform>().rect.width / (slider.maxValue / ticksPerInterval);

        for (int i = 1; i <= totalTicks; i++)
        {
            GameObject tick = Instantiate(tickMarkPrefab, slider.transform.Find("TickMarks"));
            tick.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 20); // 두께: 2px, 높이: 20px
        }
    }
}