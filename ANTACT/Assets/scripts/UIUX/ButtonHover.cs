using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text btnText;

    private Image btnImage;

    
    public Color normalTextColor = Color.black;
    public Color hoverTextColor = new Color(0,0,0);


    public Color normalBackgroundColor = Color.white;
    public Color hoverBackgroundColor = new Color(0,0,0);

    void Start()
    {
        btnText = GetComponentInChildren<Text>();
        if(btnText != null)
        {
            btnText.color = normalTextColor;
        }
        
        btnImage = GetComponent<Image>();
        if(btnImage != null)
        {
            btnImage.color = normalBackgroundColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(btnText != null)
        {
            btnText.color = hoverTextColor;
        }
        if(btnImage != null)
        {
            btnImage.color = hoverBackgroundColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(btnText != null)
        {
            btnText.color = normalTextColor;
        }
        if(btnImage != null)
        {
            btnImage.color = normalBackgroundColor;
        }
    }
}

