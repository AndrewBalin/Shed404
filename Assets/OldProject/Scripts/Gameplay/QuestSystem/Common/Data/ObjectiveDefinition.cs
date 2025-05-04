using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.QuestSystem.Common.Data
{
    [Serializable]
    public class ObjectiveDefinition
    {
        [SerializeField] private string _objectiveId;
        [SerializeField] private string _parameter;
        [SerializeField] private QuestObjectiveType _type;
        [SerializeField] private List<string> _nextObjectiveIds;

        public string ObjectiveId => _objectiveId;
        public string Parameter => _parameter;
        public QuestObjectiveType Type => _type;
        public IReadOnlyList<string> NextObjectiveIds => _nextObjectiveIds;
    }
}