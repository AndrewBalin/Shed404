using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.AI.Common
{
    public interface IWaypointProvider
    {
        public IReadOnlyList<Vector3> Waypoints { get; }
    }
}