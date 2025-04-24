using UnityEngine;
using Fishing.Fish;

namespace Fishing.CastSystem
{
    public class CastPositionCalculator
    {
        private readonly float _minDist;
        private readonly float _maxDist;
        private readonly float _height;
        private readonly float _zoneRadius;

        public CastPositionCalculator(float minDist, float maxDist, float height, float zoneRadius)
        {
            _minDist = minDist;
            _maxDist = maxDist;
            _height = height;
            _zoneRadius = zoneRadius;
        }

        public Vector3 CalculatePosition(Transform origin, float power)
        {
            float distance = Mathf.Lerp(_minDist, _maxDist, power);

            return origin.position + origin.forward * distance + Vector3.up * _height;
        }

        public bool IsInZone(Vector3 position)
        {
            foreach (Collider hit in Physics.OverlapSphere(position, _zoneRadius))
            {
                if (hit.TryGetComponent(out FishingZone _))
                {
                    return true;
                }
            }

            return false;
        }
    }
}