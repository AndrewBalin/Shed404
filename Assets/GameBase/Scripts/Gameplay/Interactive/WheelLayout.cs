using UnityEngine;

public class WheelLayout : MonoBehaviour
{
    public float radius = 100f;

    void Start()
    {
        int count = transform.childCount;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            Transform btn = transform.GetChild(i);
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            btn.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}