using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class TalkWithDed3 : Quest
    {
        public GameObject ded_outside;
        
        protected override void OnQuestComplete()
        {
            ded_outside.SetActive(true);
            QuestSystem.instance.StartQuest("talk_with_ded_4");
        }
    }
}