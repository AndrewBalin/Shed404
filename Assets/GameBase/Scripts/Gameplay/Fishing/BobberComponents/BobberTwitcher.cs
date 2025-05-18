using UnityEngine;
using DG.Tweening;
using Fishing.CastSystem;

namespace Fishing.BobberComponents
{
    [RequireComponent(typeof(Transform))]
    public class BobberTwitcher : MonoBehaviour
    {
        [SerializeField] private float _amplitude = 0.1f;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _minInterval = 0.5f;
        [SerializeField] private float _maxInterval = 1.5f;
        [SerializeField] private int _vibrato = 10;
        [SerializeField] private float _elasticity = 1f;

        private CatchMeter _catchMeter;
        private bool _twitching;
        
        public void Initialize(CatchMeter meter)
        {
            _catchMeter = meter;
        }
        
        public void StartTwitch()
        {
            if (_twitching)
            {
                return;
            }
            
            _twitching = true;
            
            ScheduleNext();
        }
        
        public void StopTwitch()
        {
            _twitching = false;
            transform.DOKill(false);
            transform.localPosition = Vector3.zero;
        }

        private void ScheduleNext()
        {
            if (!_twitching)
            {
                return;
            }
            
            float delay = Random.Range(_minInterval, _maxInterval);
            
            DOVirtual.DelayedCall(delay, DoTwitch);
        }

        private void DoTwitch()
        {
            if (!_twitching)
            {
                return;
            }
            
            float norm = _catchMeter?.ProgressNormalized ?? 1f;
            float dynAmp = _amplitude * norm;
            
            transform.DOPunchPosition(Vector3.up * dynAmp, _duration, _vibrato, _elasticity);
            transform.DOShakePosition(_duration, new Vector3(dynAmp, dynAmp, dynAmp), _vibrato, randomness: 90f, snapping: false, fadeOut: true).OnComplete(ScheduleNext);
        }
    }
}