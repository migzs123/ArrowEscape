using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // link do TMP Text
    public Color normalColor = Color.white;
    public Color hoverColor = Color.cyan;
    public float normalFontSize = 36f;
    public float hoverFontSize = 32f;
    public Vector3 normalPositionOffset = Vector3.zero;
    public Vector3 hoverPositionOffset = new Vector3(0, -5, 0);

    void Start()
    {
        buttonText.color = normalColor;
        buttonText.fontSize = normalFontSize;
        buttonText.rectTransform.localPosition = normalPositionOffset;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
        buttonText.fontSize = hoverFontSize;
        buttonText.rectTransform.localPosition = hoverPositionOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;
        buttonText.fontSize = normalFontSize;
        buttonText.rectTransform.localPosition = normalPositionOffset;
    }
}