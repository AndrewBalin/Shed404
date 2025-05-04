using System.Collections.Generic;
using Movement;
using UnityEngine;

namespace GameBase.Scripts.Gameplay.DialogSystem
{
    public class PlayerTeleport : MonoBehaviour
    {
        [Tooltip("Список точек спавна: имя Transform'а используется как ключ")]
        public Transform[] spawnPoints;

        private Dictionary<string, Transform> _map;

        void Awake()
        {
            _map = new Dictionary<string, Transform>();
            foreach (var pt in spawnPoints)
                _map[pt.name] = pt;
        }
        
        public void TeleportTo(string pointName)
        {
            if (!_map.ContainsKey(pointName))
            {
                Debug.LogError($"Spawn point '{pointName}' not found");
                return;
            }

            PlayerController.Instance.agent.Warp(_map[pointName].position);
        }
    }
}