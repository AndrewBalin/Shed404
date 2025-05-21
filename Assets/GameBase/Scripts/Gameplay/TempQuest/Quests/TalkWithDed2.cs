using UnityEngine;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class TalkWithDed2 : Quest
    {
        protected override void OnQuestComplete()
        {
            QuestSystem.instance.StartQuest("find_objects_1");
        }
    }
}