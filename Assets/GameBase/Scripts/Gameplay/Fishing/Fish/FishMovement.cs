using UnityEngine;
using UnityEngine.UI;

namespace Fishing.Fish
{
    public class FishMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private Slider _fishSlider;
        [SerializeField] private float _minSpeed = 0.2f;
        [SerializeField] private float _maxSpeed = 1f;
        [SerializeField] private float _directionChangeChance = 0.5f;

        private float _currentSpeed;
        private int _direction;

        private void Start()
        {
            InitializeMovement();
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            _fishSlider.value += _direction * _currentSpeed * Time.deltaTime;

            if (_fishSlider.value <= _fishSlider.minValue || _fishSlider.value >= _fishSlider.maxValue)
            {
                ResetMovementParameters();
            }
        }
    
        private void InitializeMovement()
        {
            _fishSlider.value = Random.Range(_fishSlider.minValue, _fishSlider.maxValue);
        
            ResetMovementParameters();
        }
    
        private void ResetMovementParameters()
        {
            _direction = Random.value < _directionChangeChance ? 1 : -1;
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        }
    }
}