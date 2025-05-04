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

/*
 * Немного документации:
 *
 * В любом месте кода, где вы хотите использовать события - регистрируйте обработчик
 * GameEventManager.instance.Register("<Название события>", <Метод>);
 *
 * Для вызова события используйте
 * GameEventManager.Instance.Trigger("<Название события>");
 *
 * Для вызова события из диалога внесите в поле "next"
 * описания реплики "event:<Название события>"
 */