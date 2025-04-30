using UnityEngine;
using UnityEngine.UI;

public class CarBehavior : MonoBehaviour
{
    public GameObject Player;
    public Text gearDisplayText;

    [Header("Transmission Settings")]
    public float ReverseSpeed = 10f;
    public float FirstGearSpeed = 10f;
    public float SecondGearSpeed = 15f;
    public float ThirdGearSpeed = 20f;
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float rotationSpeed = 80f;
    public float brakeForce = 5f;

    private float accelMltiplier = 2f;
    private float currentSpeed;
    private float targetSpeed;
    private int currentGear = 2; // 1-задняя, 2-нейтраль, 3-1я, 4-2я, 5-3я
    private bool isBraking;
    private bool OnGrasp;

    void FixedUpdate()
    {
        HandleInput();
        UpdateGear();
        UpdateMovement();
        Debug.Log(targetSpeed);
    }

    void HandleInput()
    {
        // Переключение передач
        if (Input.GetKeyDown(KeyCode.UpArrow)) ShiftUp();
        if (Input.GetKeyDown(KeyCode.DownArrow)) ShiftDown();
    }

    void UpdateGear()
    {
        // Обновление отображения передачи
        if (gearDisplayText != null)
        {
            gearDisplayText.text = GetGearDisplayString();
        }
    }

    void UpdateMovement()
    {
        HandleAcceleration();
        ApplyRotation();
        ApplyMovement();
    }

    void HandleAcceleration()
    {
        float ForwardInput = Input.GetKey(KeyCode.W) ? 1 : 0;
        float BackInput = Input.GetKey(KeyCode.S) ? 1 : 0;
        if (ForwardInput == 1) isBraking = Input.GetKey(KeyCode.S);
        if (BackInput == 1) isBraking = Input.GetKey(KeyCode.W);
        if (currentGear > 2) acceleration = accelMltiplier * (currentGear - 2); // ускорение в зависимости от передачи
        OnGrasp = Input.GetKey(KeyCode.Space); // Сцепление

        switch (currentGear)
        {
            case 1: // Задняя
                if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, -ReverseSpeed * BackInput, acceleration*2 * Time.deltaTime);
                //else if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
                else targetSpeed = 0;
                break;

            case 2: // Нейтраль
                targetSpeed = currentSpeed;
                if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
                break;

            case 3: // 1я FirstGearSpeed
                if (ForwardInput == 1) targetSpeed = Mathf.Lerp(targetSpeed, FirstGearSpeed * ForwardInput, acceleration * Time.deltaTime);
                else if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
                break;

            case 4: // 2я SecondGearSpeed
                if (ForwardInput == 1) targetSpeed = Mathf.Lerp(targetSpeed, SecondGearSpeed * ForwardInput, acceleration * Time.deltaTime);
                else if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
                break;

            case 5: // 3я ThirdGearSpeed
                if (ForwardInput == 1) targetSpeed = Mathf.Lerp(targetSpeed, ThirdGearSpeed * ForwardInput, acceleration * Time.deltaTime);
                else if (!isBraking) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
                break;
        }

        // Торможение
        if (isBraking)
        {
            targetSpeed = Mathf.Lerp(targetSpeed, 0, brakeForce * Time.deltaTime);
        }

        // Плавное изменение скорости
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
    }

    void ApplyMovement()
    {
        transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
    }

    void ApplyRotation()
    {
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation * Mathf.Sign(currentSpeed), 0);
        }
    }

    void ShiftUp()
    {
        if (currentGear < 5 && OnGrasp)
        {
            if (currentGear == 1 && Mathf.Abs(currentSpeed) > 0.1f) return;
            currentGear = Mathf.Clamp(currentGear + 1, 1, 5);
        }
    }

    void ShiftDown()
    {
        if (currentGear > 1 && OnGrasp)
        {
            if (currentGear == 3 && currentSpeed > FirstGearSpeed) return;
            currentGear = Mathf.Clamp(currentGear - 1, 1, 5);
        }
    }

    string GetGearDisplayString()
    {
        return currentGear switch
        {
            1 => "R",
            2 => "N",
            3 => "1",
            4 => "2",
            5 => "3",
            _ => "?"
        };
    }
}