using System.Collections.Generic;
using Gameplay.QuestSystem.Common.Data;
using UnityEngine;

namespace Gameplay.QuestSystem.Common.SO
{
    [CreateAssetMenu(menuName = "Quests/QuestDefinition", fileName = "QuestDefinition")]
    public class QuestDefinition : ScriptableObject
    {
        [SerializeField] private string _questId;
        [SerializeField] private List<ObjectiveDefinition> _objectives;

        public string QuestId => _questId;
        public List<ObjectiveDefinition> Objectives => _objectives;
    }
}