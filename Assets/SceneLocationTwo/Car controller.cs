using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController Instance;
    
    [Header("Wheel Settings")]
    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;
    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR;
    [SerializeField] private float _maxAngle = 35f;
    public bool fullPrivodActiv = false;

    [Header("Transmission Settings")]
    [SerializeField] private int _forwardGears = 5;
    [SerializeField] private int _reverseGears = 2;
    [SerializeField] private float[] _forwardForces = new float[] { 1000f, 1500f, 2000f, 2500f, 3000f };
    [SerializeField] private float[] _reverseForces = new float[] { 800f, 1200f };

    [Header("Engine Objects")]
    [SerializeField] private GameObject _engineStartSoundObject;
    [SerializeField] private GameObject _engineRunningSoundObject;
    [SerializeField] private GameObject _gearShiftSoundObject;
    [SerializeField] private GameObject _engineParticlesObject;

    private enum GearState { Neutral, Reverse, Forward }
    private GearState _currentGear = GearState.Neutral;
    private int _currentGearIndex = 0;
    private bool _engineRunning = false;
    private bool _clutchPressed = false;

    private void Start()
    {
        SetActive(_engineStartSoundObject, false);
        SetActive(_engineRunningSoundObject, false);
        SetActive(_gearShiftSoundObject, false);
        SetActive(_engineParticlesObject, false);
    }

    private void Update()
    {
        HandleEngineStart();
        HandleGearShift();
    }

    private void FixedUpdate()
    {
        if (!_engineRunning) return;

        ApplyMotorTorque();
        ApplyBrakes();
        ApplySteering();
        UpdateWheelVisuals();
    }
    
    public void EnableControl()
    {
        enabled = true;
    }

    private void HandleEngineStart()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!_engineRunning)
            {
                // ������ ���������
                _engineRunning = true;
                SetActive(_engineStartSoundObject, true);
                SetActive(_engineParticlesObject, true);
                Invoke(nameof(FinishEngineStart), 2f);
            }
            else
            {
                // ��������� ���������
                _engineRunning = false;
                SetActive(_engineRunningSoundObject, false);
                SetActive(_engineParticlesObject, false);
            }
        }
    }

    private void FinishEngineStart()
    {
        SetActive(_engineStartSoundObject, false);
        //SetActive(_engineParticlesObject, false);
        SetActive(_engineRunningSoundObject, true);
    }

    private void HandleGearShift()
    {
        _clutchPressed = Input.GetKey(KeyCode.LeftControl);

        if (_clutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.E)) ShiftUp();
            if (Input.GetKeyDown(KeyCode.Q)) ShiftDown();
        }
    }

    private void ShiftUp()
    {
        switch (_currentGear)
        {
            case GearState.Reverse:
                if (_currentGearIndex < _reverseGears - 1)
                    _currentGearIndex++;
                else
                    SetGear(GearState.Neutral, 0);
                break;
            case GearState.Neutral:
                SetGear(GearState.Forward, 0);
                break;
            case GearState.Forward:
                if (_currentGearIndex < _forwardGears - 1)
                    _currentGearIndex++;
                break;
        }
        PlayGearShiftSound();
    }

    private void ShiftDown()
    {
        switch (_currentGear)
        {
            case GearState.Forward:
                if (_currentGearIndex > 0)
                    _currentGearIndex--;
                else
                    SetGear(GearState.Neutral, 0);
                break;
            case GearState.Neutral:
                SetGear(GearState.Reverse, _reverseGears - 1);
                break;
            case GearState.Reverse:
                if (_currentGearIndex > 0)
                    _currentGearIndex--;
                break;
        }
        PlayGearShiftSound();
    }

    private void SetGear(GearState newGear, int newIndex)
    {
        _currentGear = newGear;
        _currentGearIndex = newIndex;
    }

    private void PlayGearShiftSound()
    {
        SetActive(_gearShiftSoundObject, true);
        Invoke(nameof(DisableGearShiftSound), 0.5f);
    }

    private void DisableGearShiftSound()
    {
        SetActive(_gearShiftSoundObject, false);
    }

    private void ApplyMotorTorque()
    {
        float currentForce = GetCurrentForce();
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : 0f;

        if (_currentGear == GearState.Reverse)
            verticalInput = -verticalInput;

        float torque = currentForce * verticalInput;

        _colliderFL.motorTorque = torque;
        _colliderFR.motorTorque = torque;

        if (fullPrivodActiv)
        {
            _colliderBL.motorTorque = torque * 0.7f;
            _colliderBR.motorTorque = torque * 0.7f;
        }
        else
        {
            _colliderBL.motorTorque = 0f;
            _colliderBR.motorTorque = 0f;
        }
    }

    private float GetCurrentForce()
    {
        switch (_currentGear)
        {
            case GearState.Forward: return _forwardForces[_currentGearIndex];
            case GearState.Reverse: return _reverseForces[_currentGearIndex];
            default: return 0f;
        }
    }

    private void ApplyBrakes()
    {
        float brakeTorque = Input.GetKey(KeyCode.Space) ? 3000f : 0f;
        _colliderFL.brakeTorque = brakeTorque;
        _colliderFR.brakeTorque = brakeTorque;
        _colliderBL.brakeTorque = brakeTorque;
        _colliderBR.brakeTorque = brakeTorque;
    }

    private void ApplySteering()
    {
        float steerAngle = _maxAngle * Input.GetAxis("Horizontal");
        _colliderFL.steerAngle = steerAngle;
        _colliderFR.steerAngle = steerAngle;
    }

    private void UpdateWheelVisuals()
    {
        UpdateWheel(_colliderFL, _transformFL);
        UpdateWheel(_colliderFR, _transformFR);
        UpdateWheel(_colliderBL, _transformBL);
        UpdateWheel(_colliderBR, _transformBR);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheelTransform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    private void SetActive(GameObject obj, bool state)
    {
        if (obj != null)
            obj.SetActive(state);
    }

    public string GetCurrentGearDisplay()
    {
        return _currentGear switch
        {
            GearState.Forward => $"D{_currentGearIndex + 1}",
            GearState.Reverse => $"R{_currentGearIndex + 1}",
            _ => "N"
        };
    }
}