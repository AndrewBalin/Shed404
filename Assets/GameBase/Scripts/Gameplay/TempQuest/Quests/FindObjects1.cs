using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class FindObjects1 : Quest
    {
        public GameObject ded;
        protected override void OnQuestComplete()
        {
            ded.SetActive(false);
            QuestSystem.instance.StartQuest("talk_with_ded_3");
        }
    }
}