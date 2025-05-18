using UnityEngine;
using UnityEngine.UI;
using Fishing.CastSystem;

namespace Fishing.PlayerController
{
    public class PlayerFishing : MonoBehaviour
    {
        [SerializeField] private int _primaryMouseButtonIndex = 0;
    
        [Header("References")]
        [SerializeField] private CatchMeter _catchMeter;
        [SerializeField] private Slider _catchSlider;
    
        [Header("Input Response")]
        [SerializeField] private float _riseAmount = 1f;
        [SerializeField] private float _fallSpeed = 0.5f;
        [SerializeField] private float _smoothTime = 0.1f;
    
        private float _velocity = 0f;
        private float _targetValue;
        private bool _hasStarted = false;
    
        private void Start()
        {
            _targetValue = _catchSlider.value;
        }
    
        private void Update()
        {
            HandlePlayerInput();
        }

        private void HandlePlayerInput()
        {
            if (!_hasStarted && Input.GetMouseButtonDown(_primaryMouseButtonIndex))
            {
                _catchMeter.StartFishing();
                _hasStarted = true;
            }
        
            if (Input.GetMouseButtonDown(_primaryMouseButtonIndex))
            {
                _targetValue += _riseAmount;
            }
        
            _targetValue -= _fallSpeed * Time.deltaTime;
        
            _targetValue = Mathf.Clamp(_targetValue, _catchSlider.minValue, _catchSlider.maxValue);
            _catchSlider.value = Mathf.SmoothDamp(_catchSlider.value, _targetValue, ref _velocity, _smoothTime);
        }
    }
}