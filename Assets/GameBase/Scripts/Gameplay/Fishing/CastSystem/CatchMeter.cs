using System;
using GameBase.Scripts.Gameplay.TempQuest;
using UnityEngine;
using UnityEngine.UI;

namespace Fishing.CastSystem
{
    public class CatchMeter : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private Slider _catchZoneSlider;
        [SerializeField] private Slider _fishSlider;
        [SerializeField] private Slider _progressSlider;

        [Header("Rates")]
        [SerializeField] private float _fillRate = 0.5f;
        [SerializeField] private float _drainRate = 0.2f;
        [SerializeField] private float _maxIdleTime = 10f;

        [Header("Catch Zone Settings")]
        [SerializeField] private float _handleSize = 50f;
        [Range(0f, 1f)] [SerializeField] private float _outOfZoneAlpha = 0.5f;

        private RectTransform _catchRect;
        private RectTransform _zoneCatch;
        private Image _catchImage;

        private float _idleTimer = 0f;
        private bool _isActive = false;

        public event Action<bool> FishingEnded;

        public float ProgressNormalized => Mathf.InverseLerp(_progressSlider.minValue, _progressSlider.maxValue, _progressSlider.value);

        private void Awake()
        {
            _catchRect = _catchZoneSlider.GetComponent<RectTransform>();
            _zoneCatch = _catchZoneSlider.handleRect;
            _catchImage = _zoneCatch.GetComponent<Image>();
        }

        private void Start()
        {
            _progressSlider.value = 0f;
            ConfigureHandleSize();
        }

        private void Update()
        {
            ProcessCatch();
        }

        public void StartFishing()
        {
            _progressSlider.value = 0f;
            _idleTimer = 0f;
            _isActive = true;
        }

        private void ProcessCatch()
        {
            if (!_isActive)
            {
                return;
            }

            bool inZone = IsFishInZone();

            UpdateHandleAlpha(inZone);
            UpdateProgressValue(inZone);
            CheckCompletion(inZone);
        }

        private bool IsFishInZone()
        {
            float distance = Mathf.Abs(_fishSlider.value - _catchZoneSlider.value);
            float sliderHeight = _catchRect.rect.height;
            float unitsPerPixel = (_catchZoneSlider.maxValue - _catchZoneSlider.minValue) / sliderHeight;
            float halfZone = (_handleSize * 0.5f) * unitsPerPixel;

            return distance <= halfZone;
        }

        private void UpdateHandleAlpha(bool inZone)
        {
            Color color = _catchImage.color;
            color.a = inZone ? 1f : _outOfZoneAlpha;
            _catchImage.color = color;
        }

        private void UpdateProgressValue(bool inZone)
        {
            _progressSlider.value += (inZone ? _fillRate : -_drainRate) * Time.deltaTime;
            _progressSlider.value = Mathf.Clamp(_progressSlider.value, _progressSlider.minValue, _progressSlider.maxValue);
        }

        private void CheckCompletion(bool inZone)
        {
            if (_progressSlider.value <= _progressSlider.minValue)
            {
                _idleTimer += Time.deltaTime;

                if (_idleTimer >= _maxIdleTime)
                {
                    EndFishing(false);
                }
            }
            else
            {
                _idleTimer = 0f;
            }

            if (_progressSlider.value >= _progressSlider.maxValue)
            {
                EndFishing(true);
            }
        }

        private void EndFishing(bool caught)
        {
            _isActive = false;
            FishingEnded?.Invoke(caught);
            QuestSystem.instance.Action("fishing", null);
        }

        private void ConfigureHandleSize()
        {
            Vector2 size = _zoneCatch.sizeDelta;
            size.y = _handleSize;
            _zoneCatch.sizeDelta = size;
        }
    }
}