using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class TalkWithDed4 : Quest
    {
        protected override void OnQuestComplete()
        {
            QuestSystem.instance.StartQuest("find_objects_2");
        }
    }
}