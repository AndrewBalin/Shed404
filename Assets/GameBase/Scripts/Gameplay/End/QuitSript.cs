using UnityEngine;
using UnityEngine.UI; // Не забудь добавить для работы с кнопками

public class QuitScript : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Ссылка для редиректа в WebGL (оставь пустым для закрытия)")]
    public string webRedirectURL = "";

    private Button quitButton; // Ссылка на компонент кнопки

    private void Start()
    {
        // Автоматически находим кнопку на этом объекте
        quitButton = GetComponent<Button>();

        // Подписываем метод QuitGame на нажатие кнопки
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
        else
        {
            Debug.LogError("Не найден компонент Button на этом объекте!");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        if (!string.IsNullOrEmpty(webRedirectURL))
        {
            // Редирект на указанный URL
            Application.ExternalEval($"window.location.href = '{webRedirectURL}';");
        }
        else
        {
            // Попытка закрыть вкладку (работает не во всех браузерах)
            Application.ExternalEval("window.close();");
        }
#else
        Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        // Важно отписаться от события при уничтожении объекта
        if (quitButton != null)
        {
            quitButton.onClick.RemoveListener(QuitGame);
        }
    }
}