using UnityEngine;
using TMPro;

public class SpeedTracker : MonoBehaviour
{
    [SerializeField] private RectTransform speedometerNeedle;

    [SerializeField] private float maxSpeed = 100f;

    [SerializeField] private float minNeedleAngle = -90f; 
    [SerializeField] private float maxNeedleAngle = 90f;  
    private Vector3 previousPosition;
    private float currentSpeed;

    void Start()
    {
        previousPosition = transform.position;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(previousPosition, transform.position);

        currentSpeed = distance / Time.fixedDeltaTime*3;

        previousPosition = transform.position;

        if (speedometerNeedle != null)
        {
            UpdateSpeedometerNeedle();
        }
    }

    private void UpdateSpeedometerNeedle()
    {
        float clampedSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        float needleAngle = Mathf.Lerp(minNeedleAngle, maxNeedleAngle, clampedSpeed / maxSpeed);

        speedometerNeedle.localRotation = Quaternion.Euler(0, 0, needleAngle);
    }
}
