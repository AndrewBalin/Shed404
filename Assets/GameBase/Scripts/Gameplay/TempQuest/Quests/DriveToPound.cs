using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class DriveToPound : Quest
    {
        public GameObject ded;
        public GameObject car;
        protected override void OnQuestComplete()
        {
            ded.SetActive(true);
            car.SetActive(false);
            QuestSystem.instance.StartQuest("fishing");
        }
    }
}