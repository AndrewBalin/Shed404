namespace Gameplay.AI.Common
{
    public interface IWaypointSelector
    {
        public int GetNextIndex(int currentIndex, int waypointsCount);
    }
}