using UnityEngine;
using UnityEngine.UI;

public class CarBehavior : MonoBehaviour
{
    public Text gearDisplayText;
    public Text graspDisplayText;

    [Header("Transmission Settings")]
    public float acceleration = 1f;
    public float deceleration = 1f;
    public float rotationSpeed = 2f;
    public float brakeForce = 5f;

    public float currentSpeed { get; private set; }
    private float targetSpeed;
    public int currentGear { get; private set; } = 2;
    public bool isBraking { get; private set; }
    private bool isClutchPressed;

    void FixedUpdate()
    {
        HandleInput();
        UpdateInformation();
        //UpdateMovement();
    }

    void HandleInput()
    {
        // Управление сцеплением
        isClutchPressed = Input.GetKey(KeyCode.Space);

        // Переключение передач только при нажатом сцеплении
        if (isClutchPressed)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) ShiftUp();
            if (Input.GetKeyDown(KeyCode.DownArrow)) ShiftDown();
        }
    }

    void ShiftUp()
    {
        if (currentGear >= 5) return;

        // Проверки для безопасного переключения
        if (currentGear == 1 && Mathf.Abs(currentSpeed) > 0.1f) return;
        if (currentGear >= 2 && currentSpeed > GetMaxSpeedForGear(currentGear) + 2f) return;

        currentGear++;
    }

    void ShiftDown()
    {
        if (currentGear <= 1) return;

        // Проверка для безопасного переключения на заднюю передачу
        if (currentGear == 2 && currentSpeed > 0.1f) return;

        currentGear--;
    }

    void UpdateInformation()
    {
        if (gearDisplayText != null)
            gearDisplayText.text = GetGearDisplayString();
        if (graspDisplayText != null)
            graspDisplayText.text = isClutchPressed ? "Сцепление ON" : "Сцепление OFF";
    }

    // Заменяем UpdateMovement() на:
    public float GetTargetSpeed()
    {
        if (isBraking) return 0f;

        return currentGear switch
        {
            1 => Input.GetKey(KeyCode.S) ? -5f : 0f,
            2 => 0f,
            _ => Input.GetKey(KeyCode.W) ? 5 * (currentGear - 2) : 0f
        };
    }

    float GetMaxSpeedForGear(int gear)
    {
        return 5 * (gear - 2); // 3 передача: 5, 4 передача: 10, 5 передача: 15
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