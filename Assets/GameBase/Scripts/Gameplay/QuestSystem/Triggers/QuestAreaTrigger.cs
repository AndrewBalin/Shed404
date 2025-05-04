using System;
using UnityEngine;
using Gameplay.QuestSystem.Common.Interfaces;
using Movement;

namespace Gameplay.QuestSystem.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class QuestAreaTrigger : MonoBehaviour, IQuestTrigger
    {
        [SerializeField] private string _questId;
        [SerializeField] private string _objectiveId;

        public event Action<string,string> Triggered = delegate {};

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController _))
                Triggered(_questId, _objectiveId);
        }
    }
}