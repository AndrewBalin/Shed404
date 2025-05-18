using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DraggableImage : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [Header("Настройки границ")]
    public RectTransform boundary; // Область, в которой можно перемещаться
    public Image backgroundImage; // Ссылка на изображение, которое будем двигать

    private RectTransform rectTransform;
    private Vector2 offset;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset);

        offset = rectTransform.anchoredPosition - offset;
        //canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            boundary,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localCursor))
        {
            // Ограничиваем позицию в рамках boundary
            Vector2 clampedPosition = GetClampedPosition(localCursor + offset);
            rectTransform.anchoredPosition = clampedPosition;
        }
    }

    private Vector2 GetClampedPosition(Vector2 targetPosition)
    {
        if (boundary == null) return targetPosition;

        // Получаем границы перемещения
        Vector2 min = boundary.rect.min - rectTransform.rect.min;
        Vector2 max = boundary.rect.max - rectTransform.rect.max;

        return new Vector2(
            Mathf.Clamp(targetPosition.x, min.x, max.x),
            Mathf.Clamp(targetPosition.y, min.y, max.y)
        );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}