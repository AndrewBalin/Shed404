using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class TalkWithDed5 : Quest
    {
        public GameObject ded;
        protected override void OnQuestComplete()
        {
            ded.SetActive(false);
            QuestSystem.instance.StartQuest("drive_to_pound");
        }
    }
}