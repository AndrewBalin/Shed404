using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WheelButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = inactiveSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = activeSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = inactiveSprite;
    }
}