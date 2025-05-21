using System;
using System.Collections.Generic;
using Dialogs.Scripts;
using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest
{
    public enum QuestType
    {
        Collection,
        Dialog,
        Exploration
    }
    
    [System.Serializable]
    public class QuestItem
    {
        public string itemName;
        public GameObject itemObject;
        public GameObject itemToDestroy;
    }
    
    [System.Serializable]
    public class Quest : MonoBehaviour
    {
        [SerializeField] private QuestType questType;
        
        [SerializeField] private string questTitle;
        [SerializeField] private string description;
        
        [SerializeField] private List<QuestItem> itemsToCollect;
        private List<GameObject> items => itemsToCollect.ConvertAll(item => item.itemObject);
        
        [SerializeField] private string[] dialogKeys;
        
        [SerializeField] private GameObject locationsToVisit;
        
        private bool _isComplete;
        private bool _isActive;
        
        public bool IsComplete => _isComplete;
        public bool IsActive => _isActive;
        
        private void OnQuestStart()
        {
        }
        
        public void QuestStart()
        {
            _isComplete = false;
            _isActive = true;
            Debug.Log($"Квест '{questTitle}' начат.");
            OnQuestStart();
        }

        protected virtual void OnQuestComplete()
        {
        }
    
        public void QuestComplete()
        {
            _isComplete = true;
            _isActive = false;
            Debug.Log($"Квест '{questTitle}' завершен.");
            OnQuestComplete();
        }

        public bool Action(String action_name, GameObject action_object)
        {
            if (questType == QuestType.Collection && action_name == "CollectItem")
            {
                if (items.Contains(action_object))
                {
                    foreach (var item in itemsToCollect)
                    {
                        if (item.itemObject == action_object)
                        {
                            switch (item.itemName)
                            {
                                case "wheels":
                                    DialogManager.instance.StartDialog("wheels");
                                    break;
                                case "core":
                                    DialogManager.instance.StartDialog("core");
                                    break;
                                case "akkumulator":
                                    DialogManager.instance.StartDialog("batareyka");
                                    break;
                            }
                            
                            item.itemToDestroy.SetActive(false);
                            itemsToCollect.Remove(item);
                            break;
                        }
                    }
                    if (itemsToCollect.Count == 0)
                    {
                        QuestComplete();
                    }
                    return true;
                }
            }

            return false;
        }
        
    }
}