using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.QuestSystem.Common.Interfaces;
using Gameplay.QuestSystem.Common.SO;

namespace Gameplay.QuestSystem.Runtime
{
    public class QuestService : IQuestService
    {
        private readonly Dictionary<string, QuestDefinition> _allQuests;
        private readonly Dictionary<string, QuestRuntime> _activeQuests;

        public QuestService(IEnumerable<QuestDefinition> definitions)
        {
            _allQuests = definitions.ToDictionary(q => q.QuestId);
            _activeQuests = new Dictionary<string, QuestRuntime>();
        }

        public void StartQuest(string questId)
        {
            if (!_allQuests.ContainsKey(questId))
                throw new Exception($"Quest {questId} not found");
            
            QuestDefinition def = _allQuests[questId];
            var runtime = new QuestRuntime(def);
            
            _activeQuests[questId] = runtime;
            
            runtime.Start();
        }

        public void CompleteObjective(string questId, string objectiveId)
        {
            if (!_activeQuests.TryGetValue(questId, out var runtime))
                return;
            
            runtime.MarkObjectiveCompleted(objectiveId);
            
            if (runtime.IsCompleted)
            {
                _activeQuests.Remove(questId);
            }
        }

        public bool IsQuestActive(string questId) => _activeQuests.ContainsKey(questId);
    }
}