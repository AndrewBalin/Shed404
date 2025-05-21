using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest
{
    public class QuestSystem : MonoBehaviour
    {
        [System.Serializable]
        public class QuestEntry
        {
            public string questName;
            public Quest quest;
        }
        
        public List<QuestEntry> quests;
        public static QuestSystem instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void StartQuest(String questName)
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.questName == questName)
                {
                    questEntry.quest.QuestStart();
                    Debug.Log($"Quest '{questName}' started.");
                    return;
                }
            }
            Debug.LogWarning($"Quest '{questName}' not found.");
        }
        
        public void CompleteQuest(String questName)
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.questName == questName)
                {
                    questEntry.quest.QuestComplete();
                    Debug.Log($"Quest '{questName}' completed.");
                    return;
                }
            }
            Debug.LogWarning($"Quest '{questName}' not found.");
        }

        public bool IsQuestComplete(String questName)
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.questName == questName)
                {
                    return questEntry.quest.IsComplete;
                }
            }
            Debug.LogWarning($"Quest '{questName}' not found.");
            return false;
        }

        public bool IsQuestActive(String questName)
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.questName == questName)
                {
                    return questEntry.quest.IsActive;
                }
            }
            Debug.LogWarning($"Quest '{questName}' not found.");
            return false;
        }

        public QuestEntry GetActiveQuest()
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.quest.IsActive)
                {
                    return questEntry;
                }
            }
            Debug.LogWarning($"Active quest not found.");
            return null;
        }

        public bool Action(String action_name, GameObject action_object)
        {
            foreach (var questEntry in quests)
            {
                if (questEntry.quest.IsActive)
                {
                    return questEntry.quest.Action(action_name, action_object);
                }
            }
            Debug.LogWarning($"Active quest not found.");
            return false;
        }
    }
}