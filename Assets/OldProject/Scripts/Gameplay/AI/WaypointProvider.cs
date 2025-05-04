using System.Collections.Generic;
using UnityEngine;
using Gameplay.AI.Common;

namespace Gameplay.AI
{
    public class WaypointProvider : MonoBehaviour, IWaypointProvider
    {
        [SerializeField] private Transform[] _waypoints;

        public IReadOnlyList<Vector3> Waypoints
        {
            get
            {
                Vector3[] positions = new Vector3[_waypoints.Length];
                
                for (int i = 0; i < _waypoints.Length; i++)
                    positions[i] = _waypoints[i].position;
                
                return positions;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_waypoints == null) return;
            
            Gizmos.color = Color.yellow;
            
            for (int i = 0; i < _waypoints.Length; i++)
            {
                if (_waypoints[i] == null)
                    continue;
                
                Gizmos.DrawSphere(_waypoints[i].position, 0.2f);
                
                if (i + 1 < _waypoints.Length && _waypoints[i + 1] != null)
                    Gizmos.DrawLine(_waypoints[i].position, _waypoints[i + 1].position);
            }
        }
    }
}