using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fishing.BobberComponents;

namespace Fishing.CastSystem
{
    public class CastController : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private KeyCode _castKey = KeyCode.E;

        [Header("UI References")]
        [SerializeField] private CatchMeter _catchMeter;
        [SerializeField] private RectTransform _fishingPanel;
        [SerializeField] private TextMeshProUGUI _castPrompt;
        [SerializeField] private Canvas _castUI;
        [SerializeField] private Slider _powerSlider;

        [Header("Power Settings")]
        [SerializeField] private Gradient _powerGradient;
        [SerializeField] private float _powerSpeed = 0.5f;
        [SerializeField] private float _powerRange = 1f;

        [Header("Bobber Prefabs")]
        [SerializeField] private Bobber _bobberPrefab;
        [SerializeField] private Bobber _bobberPreviewPrefab;

        [Header("Cast Mechanics")]
        [SerializeField] private Transform _castOrigin;
        [SerializeField] private float _minCastDistance = 5f;
        [SerializeField] private float _maxCastDistance = 20f;
        [SerializeField] private float _castHeight = 0f;
        [SerializeField] private float _zoneCheckRadius = 0.1f;

        private CastPowerCalculator _powerCalculator;
        private CastPositionCalculator _positionCalculator;
        private Bobber _previewBobber;
        private Bobber _realBobber;
        private bool _isFishingActive = false;
        private bool _isCasting = false;

        private void Start()
        {
            _powerCalculator = new CastPowerCalculator(_powerSpeed, _powerRange);
            _positionCalculator = new CastPositionCalculator(_minCastDistance, _maxCastDistance, _castHeight, _zoneCheckRadius);
            
            SetupInitialUI();
        }

        private void OnEnable()
        {
            _catchMeter.FishingEnded += OnFishingEnd;
        }

        private void OnDisable()
        {
            _catchMeter.FishingEnded -= OnFishingEnd;
        }

        private void Update()
        {
            ProcessCastInput();
        }

        private void ProcessCastInput()
        {
            if (_isFishingActive)
            {
                return;
            }

            if (!_isCasting)
            {
                if (Input.GetKeyDown(_castKey))
                {
                    StartCast();
                }
            }
            else
            {
                if (Input.GetKey(_castKey))
                {
                    UpdateCastPreview();
                }
                else if (Input.GetKeyUp(_castKey))
                {
                    ReleaseCast();
                }
            }
        }

        private void StartCast()
        {
            _isCasting = true;
            _powerCalculator.Reset();

            _castPrompt.gameObject.SetActive(false);
            _castUI.gameObject.SetActive(true);
            _powerSlider.value = 0f;

            if (_bobberPreviewPrefab != null && _castOrigin != null)
            {
                _previewBobber = Instantiate(_bobberPreviewPrefab, _castOrigin.position, _castOrigin.rotation);
            }
        }

        private void UpdateCastPreview()
        {
            float power = _powerCalculator.Update(Time.deltaTime);
            _powerSlider.value = power;

            if (_powerSlider.fillRect.TryGetComponent(out Image img))
            {
                img.color = _powerGradient.Evaluate(power);
            }

            if (_previewBobber != null)
            {
                Vector3 position = _positionCalculator.CalculatePosition(_castOrigin, power);
                _previewBobber.transform.position = position;
            }
        }

        private void ReleaseCast()
        {
            _isCasting = false;

            _castUI.gameObject.SetActive(false);

            if (_previewBobber == null)
            {
                _castPrompt.gameObject.SetActive(true);

                return;
            }

            Vector3 spawnPosition = _previewBobber.transform.position;
            Quaternion spawnRotation = _previewBobber.transform.rotation;
            
            Destroy(_previewBobber.gameObject);

            if (!_positionCalculator.IsInZone(spawnPosition))
            {
                _castPrompt.gameObject.SetActive(true);

                return;
            }

            _realBobber = Instantiate(_bobberPrefab, spawnPosition, spawnRotation);

            if (_realBobber.TryGetComponent(out Rigidbody rigidbodyBobber))
            {
                rigidbodyBobber.isKinematic = true;
                rigidbodyBobber.useGravity = false;
            }

            if (_realBobber.TryGetComponent(out BobberTwitcher twitcher))
            {
                twitcher.Initialize(_catchMeter);
                twitcher.StartTwitch();
            }

            _fishingPanel.gameObject.SetActive(true);
            _catchMeter.StartFishing();
            _isFishingActive = true;
        }

        private void OnFishingEnd(bool caught)
        {
            _isFishingActive = false;
            _fishingPanel.gameObject.SetActive(false);

            if (_realBobber)
            {
                if (_realBobber.TryGetComponent(out BobberTwitcher twitcher))
                {
                    twitcher.StopTwitch();
                }

                Destroy(_realBobber.gameObject);
            }

            _castPrompt.gameObject.SetActive(true);
        }

        private void SetupInitialUI()
        {
            _castPrompt.gameObject.SetActive(true);
            _castUI.gameObject.SetActive(false);
            _fishingPanel.gameObject.SetActive(false);
        }
    }
}