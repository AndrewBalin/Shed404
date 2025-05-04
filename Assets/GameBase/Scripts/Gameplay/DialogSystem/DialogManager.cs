using System.Collections.Generic;
using GameBase.Scripts.Gameplay.DialogSystem;
using Gameplay.QuestSystem.Common.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Dialogs.Scripts
{
    public class DialogManager : MonoBehaviour
    {
        public static DialogManager instance;

        [Header("Dialog Data")]
        public TextAsset dialogJson;
        
        [Header("Game Data")]
        public GameObject player;
        public NavMeshAgent agent;
        public GameObject homeStartPosition;
        
        private Dictionary<string, DialogNodeWithId> _dialogTree;
        private DialogNodeWithId _currentNode;
        private string _currentQuestId;
        
        

        [Inject] private IQuestService _questService;
        [Inject] private ScreenFader _screenFader;
        [Inject] private PlayerTeleport _teleporter;

        void Awake()
        {
            instance = this;
            _dialogTree = JsonUtility.FromJson<Wrapper>(dialogJson.text).ToDictionary();
        }

        public void StartDialog(string startId, string questId = null)
        {
            _currentQuestId = questId;
            if (!_dialogTree.ContainsKey(startId))
            {
                Debug.LogError($"Ключ '{startId}' не найден в словаре диалогов.");
                return;
            }
            agent.destination = player.transform.position;
            ShowNode(_dialogTree[startId]);

            if (!string.IsNullOrEmpty(questId) && !_questService.IsQuestActive(questId))
                _questService.StartQuest(questId);
        }

        public void ContinueDialog()
        {
            if (_currentNode == null) return;

            string next = _currentNode.next;
            if (!string.IsNullOrEmpty(next) && next.StartsWith("event:"))
            {
                string eventId = next.Substring("event:".Length);
                TriggerEvent(eventId);
                Debug.Log("Диалог завершён.");
                DialogUI.instance.Hide();
                return;
            }

            if (!string.IsNullOrEmpty(next) && _dialogTree.ContainsKey(next))
                ShowNode(_dialogTree[next]);
            else
                DialogUI.instance.Hide();
        }

        public void SelectOption(int index)
        {
            string next = _currentNode.options[index].next;
            ShowNode(_dialogTree[next]);
        }

        void ShowNode(DialogNodeWithId node)
        {
            _currentNode = node;
            DialogUI.instance.Show(node);
        }

        void TriggerEvent(string eventId)
        {
            Debug.Log($"[EVENT]: {eventId}");
            switch (eventId)
            {
                case "teleport_to_home":
                    player.transform.position = homeStartPosition.transform.position;
                    agent.destination = homeStartPosition.transform.position;
                    return;
                case "spawn_car_drive":
                    _teleporter.TeleportTo("CarSpawnPoint");
                    _questService.CompleteObjective(_currentQuestId, eventId);
                    CarController.Instance.EnableControl();
                    break;
                default:
                    _questService.CompleteObjective(_currentQuestId, eventId);
                    break;
            }
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