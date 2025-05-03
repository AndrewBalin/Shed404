using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Scripts
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager instance;

        [Header("Dialog Data")]
        public TextAsset dialogJson;
        
        private Dictionary<string, DialogNodeWithId> _dialogTree;
        private DialogNodeWithId _currentNode;

        void Awake()
        {
            instance = this;
            _dialogTree = JsonUtility.FromJson<Wrapper>(dialogJson.text).ToDictionary();
        }

        public void StartDialog(string startId)
        {
            if (!_dialogTree.ContainsKey(startId))
            {
                Debug.LogError($"Ключ '{startId}' не найден в словаре диалогов.");
                Debug.Log(string.Join(", ", _dialogTree.Keys));
                return;
            }
            ShowNode(_dialogTree[startId]);
        }
        
        public void ContinueDialog()
        {
            if (_currentNode == null) return;

            string next = _currentNode.next;
                
            // Если хотим вызывать событие, а не переходить к следующей ноде
            if (!string.IsNullOrEmpty(next) && next.StartsWith("event:"))
            {
                string eventId = next.Substring("event:".Length);

                TriggerEvent(eventId);
                return;
            }

            if (!string.IsNullOrEmpty(next) && _dialogTree.ContainsKey(next))
            {
                ShowNode(_dialogTree[next]);
            }
            else
            {
                Debug.Log("Диалог завершён.");
                DialogUI.instance.Hide();
            }
        }

        public void SelectOption(int index)
        {
            string next = _currentNode.options[index].next;
            ShowNode(_dialogTree[next]);
        }

        void ShowNode(DialogNodeWithId node)
        {
            if (node == null)
            {
                Debug.LogError("Переданный узел диалога равен null.");
                return;
            }

            if (DialogUI.instance == null)
            {
                Debug.LogError("DialogUI.instance не инициализирован.");
                return;
            }
            _currentNode = node;
            DialogUI.instance.Show(node);
        }

        void TriggerEvent(string eventId)
        // TODO: Триггерить событие для квестовой системы (?)
        {
            Debug.Log($"[EVENT]: {eventId}");
        }

        [System.Serializable]
        private class Wrapper
        {
            public List<DialogNodeWithId> nodes;
            public Dictionary<string, DialogNodeWithId> ToDictionary()
            {
                var dict = new Dictionary<string, DialogNodeWithId>();
                foreach (var entry in nodes)
                    dict[entry.id] = entry;
                return dict;
            }
        }
    }
}