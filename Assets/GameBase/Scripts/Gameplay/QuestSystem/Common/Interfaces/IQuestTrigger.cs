using System;

namespace Gameplay.QuestSystem.Common.Interfaces
{
    public interface IQuestTrigger
    {
        public event Action<string, string> Triggered; 
    }
}