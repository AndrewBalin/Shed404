using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SimpleCanvasQuest : MonoBehaviour
{
    [Header("Основные настройки")]
    public TMP_Text questText; // Элемент TextMeshPro для списка
    public TMP_Text completionText; // Текст для сообщения о завершении
    public List<GameObject> targetObjects = new List<GameObject>();

    [Header("Тексты")]
    [TextArea(3, 10)]
    public string questDescription = "Найди следующие объекты:\n{ITEMS}";
    public string completionMessage = "Все объекты найдены!";
    public float completionDisplayTime = 3f; // Время показа сообщения

    private Dictionary<GameObject, bool> foundObjects = new Dictionary<GameObject, bool>();
    private int foundCount = 0;

    void Start()
    {
        // Инициализация
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }

        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                foundObjects[obj] = false;
                AddClickHandler(obj);
            }
        }

        UpdateQuestText();
    }

    void AddClickHandler(GameObject target)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null) trigger = target.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => OnObjectClicked(target));

        trigger.triggers.Add(entry);
    }

    void OnObjectClicked(GameObject clickedObject)
    {
        if (foundObjects.ContainsKey(clickedObject))
        {
            if (!foundObjects[clickedObject])
            {
                foundObjects[clickedObject] = true;
                foundCount++;

                UpdateQuestText();
                MarkObjectFound(clickedObject);

                // Проверяем завершение квеста
                if (foundCount == targetObjects.Count)
                {
                    ShowCompletionMessage();
                }
            }
        }
    }

    void MarkObjectFound(GameObject obj)
    {
        // Визуальное выделение найденного объекта
        Image img = obj.GetComponent<Image>();
        if (img != null)
        {
            //img.color = new Color(0.7f, 0.7f, 0.7f, 0.5f);
        }
    }

    void UpdateQuestText()
    {
        string itemsList = "";

        foreach (var obj in targetObjects)
        {
            if (obj == null) continue;

            string itemName = obj.name;
            bool found = foundObjects[obj];

            itemsList += found ? $"<s><color=#808080>{itemName}</color></s>\n" : $"{itemName}\n";
        }

        questText.text = questDescription.Replace("{ITEMS}", itemsList);
    }

    void ShowCompletionMessage()
    {
        if (completionText != null)
        {
            completionText.text = completionMessage;
            completionText.gameObject.SetActive(true);

            // Через заданное время скрываем сообщение
            Invoke("HideCompletionMessage", completionDisplayTime);
        }

        // Дополнительные действия при завершении
        Debug.Log("Квест завершен!");
    }

    void HideCompletionMessage()
    {
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    [ContextMenu("Обновить текст квеста")]
    public void EditorUpdateQuestText()
    {
        if (questText != null)
            UpdateQuestText();
    }
}