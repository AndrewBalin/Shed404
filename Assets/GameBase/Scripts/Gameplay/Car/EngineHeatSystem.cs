using UnityEngine;
using UnityEngine.UI;
using System;

public class EngineHeatSystem : MonoBehaviour
{
    [Header("Heat Settings")]
    [SerializeField] private float _overheatThreshold = 100f;
    [SerializeField] private float _heatIncrease4WD = 15f;
    [SerializeField] private float _heatIncrease4thGear = 10f;
    [SerializeField] private float _cooldownRate = 20f;
    [SerializeField] private Image _heatIndicator;

    public event Action<bool> OnOverheatChanged;

    private float _currentHeat = 0f;
    private bool _isOverheated = false;
    private bool _engineRunning = false;
    private bool _is4WDActive = false;
    private bool _is4thGearActive = false;

    public bool IsOverheated => _isOverheated;

    public void SetEngineState(bool isRunning)
    {
        _engineRunning = isRunning;
    }

    public void Set4WDState(bool isActive)
    {
        _is4WDActive = isActive;
    }

    public void SetGearState(bool is4thGear)
    {
        _is4thGearActive = is4thGear;
    }

    private void Update()
    {
        if (!_engineRunning || _isOverheated)
        {
            CoolDownEngine();
            return;
        }

        CalculateHeat();
        UpdateHeatIndicator();
    }

    private void CalculateHeat()
    {
        float heatIncrease = 0f;

        if (_is4WDActive)
        {
            heatIncrease += _heatIncrease4WD * Time.deltaTime / 60f;
        }

        if (_is4thGearActive)
        {
            heatIncrease += _heatIncrease4thGear * Time.deltaTime / 60f;
        }

        _currentHeat += heatIncrease;

        if (heatIncrease <= Mathf.Epsilon)
        {
            _currentHeat -= _cooldownRate * Time.deltaTime / 60f;
        }

        _currentHeat = Mathf.Clamp(_currentHeat, 0f, _overheatThreshold);

        if (_currentHeat >= _overheatThreshold)
        {
            OverheatEngine();
        }
    }

    private void CoolDownEngine()
    {
        if (_isOverheated)
        {
            _currentHeat -= _cooldownRate * Time.deltaTime / 60f;

            if (_currentHeat <= 0f)
            {
                _currentHeat = 0f;
                _isOverheated = false;
                OnOverheatChanged?.Invoke(false);
            }
        }
    }

    private void OverheatEngine()
    {
        if (!_isOverheated)
        {
            _isOverheated = true;
            OnOverheatChanged?.Invoke(true);
            Debug.Log("Engine overheated! Wait for cooldown.");
        }
    }

    private void UpdateHeatIndicator()
    {
        if (_heatIndicator == null) return;

        float heatPercentage = _currentHeat / _overheatThreshold;
        _heatIndicator.color = Color.Lerp(Color.green, Color.red, heatPercentage);

        if (_isOverheated)
        {
            _heatIndicator.color = Color.Lerp(Color.red, Color.white,
                Mathf.PingPong(Time.time * 2f, 1f));
        }
    }
}