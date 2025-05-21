using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class FindObjects2 : Quest
    {
        public GameObject oldDed;
        public GameObject newDed;
        
        protected override void OnQuestComplete()
        {
            oldDed.SetActive(false);
            newDed.SetActive(true);
            QuestSystem.instance.StartQuest("talk_with_ded_5");
        }
    }
}