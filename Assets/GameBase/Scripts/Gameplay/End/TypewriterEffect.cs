using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events; // Для использования UnityEvent

public class TypewriterEffect : MonoBehaviour
{
    [Header("Основные настройки")]
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private float delayBetweenChars = 0.1f;
    [SerializeField] private float delayAfterComplete = 1f;
    [SerializeField] private bool playOnStart = true;

    [Header("Настройки титров")]
    [SerializeField] private GameObject creditsPanel; // Панель с титрами
    [SerializeField] private float fadeDuration = 1f; // Длительность появления титров
    [SerializeField] private bool showCreditsAfterText = true;

    [Header("События")]
    public UnityEvent onTypingComplete; // Событие завершения печати

    private string fullText;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        fullText = textComponent.text;
        textComponent.text = "";

        if (playOnStart)
            StartTyping();

        // Скрываем панель титров в начале
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
            CanvasGroup canvasGroup = creditsPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = creditsPanel.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textComponent.text = "";
        int currentCharIndex = 0;

        while (currentCharIndex < fullText.Length)
        {
            textComponent.text += fullText[currentCharIndex];
            currentCharIndex++;
            yield return new WaitForSeconds(delayBetweenChars);
        }

        yield return new WaitForSeconds(delayAfterComplete);

        // Вызываем событие завершения
        onTypingComplete.Invoke();

        // Показываем титры, если нужно
        if (showCreditsAfterText && creditsPanel != null)
        {
            yield return StartCoroutine(ShowCredits());
        }
    }

    private IEnumerator ShowCredits()
    {
        creditsPanel.SetActive(true);
        CanvasGroup canvasGroup = creditsPanel.GetComponent<CanvasGroup>();

        // Плавное появление
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = fullText;

        // Сразу показываем титры при пропуске
        if (showCreditsAfterText && creditsPanel != null)
        {
            StartCoroutine(ShowCredits());
        }
    }
}