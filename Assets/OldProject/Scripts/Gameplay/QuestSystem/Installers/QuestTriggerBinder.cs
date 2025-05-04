using System.Collections.Generic;
using Zenject;
using Gameplay.QuestSystem.Common.Interfaces;

namespace Gameplay.QuestSystem.Installers
{
    public class QuestTriggerBinder : IInitializable
    {
        readonly IQuestService _quests;
        readonly IEnumerable<IQuestTrigger> _triggers;

        public QuestTriggerBinder(IQuestService quests, IEnumerable<IQuestTrigger> triggers)
        {
            _quests = quests;
            _triggers = triggers;
        }

        public void Initialize()
        {
            foreach (IQuestTrigger trigger in _triggers)
                trigger.Triggered += _quests.CompleteObjective;
        }
    }
}