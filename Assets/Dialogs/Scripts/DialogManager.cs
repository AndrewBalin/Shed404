using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Scripts
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager instance;

        [Header("Dialog Data")]
        public TextAsset dialogJson;
        
        private Dictionary<string, DialogNode> _dialogTree;
        private DialogNode _currentNode;

        void Awake()
        {
            instance = this;
            _dialogTree = JsonUtility.FromJson<Wrapper>(dialogJson.text).ToDictionary();
        }

        public void StartDialog(string startId)
        {
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
            if (!string.IsNullOrEmpty(_currentNode.@event))
                TriggerEvent(_currentNode.@event);
            
            ShowNode(_dialogTree[next]);
        }

        void ShowNode(DialogNode node)
        {
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
            public Dictionary<string, DialogNode> ToDictionary()
            {
                var dict = new Dictionary<string, DialogNode>();
                foreach (var entry in nodes)
                    dict[entry.id] = entry.node;
                return dict;
            }
        }

        [System.Serializable]
        private class DialogNodeWithId
        {
            public string id;
            public DialogNode node;
        }
    }
}