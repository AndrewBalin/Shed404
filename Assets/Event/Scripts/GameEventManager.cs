using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event.Scripts
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager instance;

        private Dictionary<string, Action> _eventTable = new();

        void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        public void Register(string eventId, Action callback)
        {
            if (!_eventTable.ContainsKey(eventId))
                _eventTable[eventId] = callback;
            else
                _eventTable[eventId] += callback;
        }

        public void Unregister(string eventId, Action callback)
        {
            if (_eventTable.ContainsKey(eventId))
                _eventTable[eventId] -= callback;
        }

        public void Trigger(string eventId)
        {
            if (_eventTable.TryGetValue(eventId, out var action))
                action?.Invoke();
            else
                Debug.LogWarning($"[GameEventManager] Событие '{eventId}' не найдено.");
        }
    }
}