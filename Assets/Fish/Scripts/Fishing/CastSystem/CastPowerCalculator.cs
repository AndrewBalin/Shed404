using UnityEngine;

namespace Fishing.CastSystem
{
    public class CastPowerCalculator
    {
        private readonly float _speed;
        private readonly float _range;
    
        private float _timer;

        public CastPowerCalculator(float speed, float range)
        {
            _speed = speed;
            _range = range;
            _timer = 0f;
        }

        public void Reset() => _timer = 0f;

        public float Update(float deltaTime)
        {
            _timer += deltaTime * _speed;

            return Mathf.PingPong(_timer, _range);
        }
    }
}