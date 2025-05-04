namespace Gameplay.QuestSystem.Common.Interfaces
{
    public interface IQuestService
    {
        public void StartQuest(string questId);
        
        public void CompleteObjective(string questId, string objectiveId);
        
        public bool IsQuestActive(string questId);
    }
}