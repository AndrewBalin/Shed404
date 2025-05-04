using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class ActionButtonEntry
{
    public Button button;
    public string label;
}

namespace Interactive
{
    public class ActionWheelUI : MonoBehaviour
    {
        public static ActionWheelUI Instance;

        public GameObject panel;
        public ActionButtonEntry[] buttons;

        private Interactable currentInteractable;
        private string currentAction;

        void Awake()
        {
            Instance = this;
            panel.SetActive(false);

            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;

                EventTrigger trigger = buttons[i].button.gameObject.GetComponent<EventTrigger>();
                if (trigger == null)
                    trigger = buttons[i].button.gameObject.AddComponent<EventTrigger>();

                trigger.triggers = new List<EventTrigger.Entry>();

                AddTrigger(trigger, EventTriggerType.PointerEnter, () => OnButtonHover(index));
                AddTrigger(trigger, EventTriggerType.PointerExit, () => OnButtonUnhover(index));
            }
        }

        void AddTrigger(EventTrigger trigger, EventTriggerType type, System.Action action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener((_) => action());
            trigger.triggers.Add(entry);
        }

        private void Update()
        {
            if (panel.activeSelf && Input.GetMouseButtonUp(1))
            {
                Hide();
            }
        }

        public void Show(Vector2 screenPosition, Interactable interactable)
        {
            currentInteractable = interactable;
            currentAction = null;
            panel.SetActive(true);
            panel.transform.position = screenPosition;

            foreach (var entry in buttons)
            {
                entry.button.gameObject.SetActive(true);
                var text = entry.button.GetComponentInChildren<Text>();
                if (text != null) text.text = entry.label;
            }
        }

        public void Hide()
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);

                if (currentInteractable != null && !string.IsNullOrEmpty(currentAction))
                {
                    //currentInteractable.PerformAction(currentAction);
                    // Раскомментить если используете несколько действий
                }

                currentInteractable = null;
                currentAction = null;
            }
        }

        public void OnButtonHover(int index)
        {
            currentAction = buttons[index].label;
        }

        public void OnButtonUnhover(int index)
        {
            currentAction = null;
        }
    }
}