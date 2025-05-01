using UnityEngine;
using UnityEngine.UI;

public class CarBehavior : MonoBehaviour
{
    public GameObject Player;
    public Text gearDisplayText;
    public Text graspDisplayText;

    [Header("Transmission Settings")]
    public float acceleration = 2f;
    public float deceleration = 1f;
    public float rotationSpeed = 80f;
    public float brakeForce = 5f;

    private float currentSpeed;
    private float targetSpeed;
    private int currentGear = 2; // 1-задняя, 2-нейтраль, 3-1я, 4-2я, 5-3я
    private bool isBraking;
    private bool OnGrasp;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) HandleInput(); // Сцепление
        UpdateInformation();
        UpdateMovement();
    }

    void HandleInput()
    {
        OnGrasp = true;
        // Переключение передач
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentGear < 5)
        {
                if ((currentGear == 1 && (Mathf.Abs(currentSpeed) > 0.01f) || (currentGear >= 3 && currentSpeed <= 5 * (currentGear - 2) - 2f * (currentGear - 2)))) return;
                currentGear = Mathf.Clamp(currentGear + 1, 1, 5);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentGear > 1)
        {
                if (currentGear == 2 && currentSpeed >= 0.1f) return;
                currentGear = Mathf.Clamp(currentGear - 1, 1, 5);
            
        }
        OnGrasp = false;
    }

    void UpdateInformation()
    {
        // Обновление отображения передачи
        if (gearDisplayText != null) gearDisplayText.text = GetGearDisplayString();
        if (graspDisplayText != null) graspDisplayText.text = GetGraspDisplayString();
    }

    void UpdateMovement()
    {
        float ForwardInput = Input.GetKey(KeyCode.W) ? 1 : 0;
        float BackInput = Input.GetKey(KeyCode.S) ? 1 : 0;
        if (ForwardInput == 1) isBraking = Input.GetKey(KeyCode.S);
        else if (BackInput == 1) isBraking = Input.GetKey(KeyCode.W);

        if (!isBraking) // скорость (5 * (currentGear - 2))
        {
            if (currentGear > 2) // от 3 до 5 передачи ( 1, 2, 3)
            {
                if (ForwardInput == 1) targetSpeed = Mathf.Lerp(targetSpeed, 5 * (currentGear - 2), (acceleration * (currentGear - 2)) * Time.deltaTime);
                else targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2);
            }
            else if (currentGear == 2) targetSpeed = Mathf.Lerp(targetSpeed, 0, Time.deltaTime * deceleration / 2); // 2 передача (N)
            else if (currentGear == 1 && BackInput == 1) targetSpeed = Mathf.Lerp(targetSpeed, -5, acceleration * 2 * Time.deltaTime); // 1 передача (R)
            else targetSpeed = 0;
        }
        else targetSpeed = Mathf.Lerp(targetSpeed, 0, brakeForce * Time.deltaTime); // Тормоз

        // Плавное изменение скорости
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        ApplyRotation();

        transform.Translate(currentSpeed * Time.deltaTime, 0, 0); // Применение изменений на объект
    }

    void ApplyRotation()
    {
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotation * Mathf.Sign(currentSpeed), 0);
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
    string GetGraspDisplayString()
    {
        return OnGrasp switch
        {
            true => "Сцепа ON",
            false => "Сцепа OFF"
        };
    }
}