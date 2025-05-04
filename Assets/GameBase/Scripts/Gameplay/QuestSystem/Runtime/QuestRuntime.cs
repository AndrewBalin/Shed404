using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.QuestSystem.Common.Data;
using Gameplay.QuestSystem.Common.SO;

namespace Gameplay.QuestSystem.Runtime
{
    public class QuestRuntime
    {
        private readonly QuestDefinition _definition;
        private readonly List<ObjectiveDefinition> _objectives;
        private readonly Dictionary<string, ObjectiveDefinition> _byId;
        
        private int _currentIndex = -1;
        
        public event Action<QuestRuntime, ObjectiveDefinition> ObjectiveStarted;
        public event Action<QuestRuntime, ObjectiveDefinition> ObjectiveCompleted;
        public event Action<QuestRuntime> QuestCompleted;

        public QuestRuntime(QuestDefinition definition)
        {
            _definition = definition;
            _objectives = definition.Objectives;
            _byId = _objectives.ToDictionary(o => o.ObjectiveId);
        }
        
        public string QuestId => _definition.QuestId;
        public bool IsCompleted { get; private set; }

        public void Start()
        {
            MoveToNext();
        }

        public void MarkObjectiveCompleted(string objectiveId)
        {
            if(IsCompleted)
                return;
            
            ObjectiveDefinition current = _objectives[_currentIndex];

            if(current.ObjectiveId != objectiveId)
                return;

            ObjectiveCompleted?.Invoke(this, current);
            
            MoveToNext();
        }

        private void MoveToNext()
        {
            _currentIndex++;

            if(_currentIndex < _objectives.Count)
            {
                ObjectiveDefinition next = _objectives[_currentIndex];
                
                ObjectiveStarted?.Invoke(this, next);
            }
            else
            {
                IsCompleted = true;
                
                QuestCompleted?.Invoke(this);
            }
        }
    }
}