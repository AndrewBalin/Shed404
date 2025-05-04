using UnityEngine;
using Gameplay.AI.Common;

namespace Gameplay.AI
{
    public class RandomWaypointSelector : IWaypointSelector
    {
        public int GetNextIndex(int currentIndex, int waypointsCount)
        {
            if (waypointsCount == 0)
                return -1;
            
            if (waypointsCount == 1)
                return 0;

            int next;
            do
            {
                next = Random.Range(0, waypointsCount);
            }
            while (next == currentIndex);

            return next;
        }
    }
}