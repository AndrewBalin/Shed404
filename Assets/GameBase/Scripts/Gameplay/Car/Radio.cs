using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Radio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songs;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text songTitleText; // Ссылка на UI Text
    [SerializeField] private float scrollSpeed = 50f; // Скорость прокрутки текста
    [SerializeField] private float displayDelay = 2f; // Задержка перед началом прокрутки

    private int currentSongIndex = 0;
    private Coroutine scrollCoroutine;
    private string fullSongName;

    public delegate void OnSongChangedHandler(AudioClip newSong);
    public event OnSongChangedHandler OnSongChanged;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        if (songTitleText == null)
        {
            Debug.LogWarning("Не назначен UI Text для отображения названия песни!");
        }

        PlayCurrentSong();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PreviousSong();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            NextSong();
        }
    }

    private void NextSong()
    {
        if (songs.Count == 0) return;

        currentSongIndex = (currentSongIndex + 1) % songs.Count;
        PlayCurrentSong();
    }

    private void PreviousSong()
    {
        if (songs.Count == 0) return;

        currentSongIndex--;
        if (currentSongIndex < 0) currentSongIndex = songs.Count - 1;
        PlayCurrentSong();
    }

    private void PlayCurrentSong()
    {
        if (currentSongIndex >= 0 && currentSongIndex < songs.Count)
        {
            audioSource.Stop();
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();

            OnSongChanged?.Invoke(songs[currentSongIndex]);
            UpdateSongTitle(songs[currentSongIndex].name);
            CheckForSpecificSongEvents(songs[currentSongIndex]);
        }
    }

    private void UpdateSongTitle(string songName)
    {
        fullSongName = songName;

        // Останавливаем предыдущую корутину, если она есть
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }

        // Сначала показываем полное название без прокрутки
        songTitleText.text = fullSongName;

        // Запускаем прокрутку после задержки
        scrollCoroutine = StartCoroutine(ScrollSongTitle());
    }

    private IEnumerator ScrollSongTitle()
    {
        // Ждем перед началом прокрутки
        yield return new WaitForSeconds(displayDelay);

        // Если текст помещается, не прокручиваем
        if (IsTextFitting(fullSongName))
        {
            yield break;
        }

        // Добавляем пробелы в конце для эффекта "ухода" текста
        string scrollingText = fullSongName + "     ";
        float textPosition = 0;

        while (true)
        {
            // Вычисляем видимую часть текста
            int startChar = Mathf.FloorToInt(textPosition);
            int visibleChars = Mathf.CeilToInt(GetTextWidth(fullSongName) / songTitleText.fontSize);

            string visibleText = "";
            for (int i = 0; i < visibleChars; i++)
            {
                int currentCharPos = (startChar + i) % scrollingText.Length;
                visibleText += scrollingText[currentCharPos];
            }

            songTitleText.text = visibleText;

            // Двигаем позицию
            textPosition += scrollSpeed * Time.deltaTime;
            if (textPosition > scrollingText.Length)
            {
                textPosition = 0;
            }

            yield return null;
        }
    }

    private bool IsTextFitting(string text)
    {
        // Простая проверка - если название короче N символов, не прокручиваем
        // Можно заменить на более точную проверку с учетом размера UI элемента
        return text.Length <= 10; // Эмпирическое значение, подберите под ваш UI
    }

    private float GetTextWidth(string text)
    {
        // Примерная оценка ширины текста
        // Для точного расчета лучше использовать TextGenerator
        return text.Length * songTitleText.fontSize * 0.6f;
    }

    private void CheckForSpecificSongEvents(AudioClip currentSong)
    {
        if (currentSong.name == "Now_Play_No_Name_Techno_Remix_by_akxmo")
        {
            Debug.Log("Играет типичный чёрный рэп!");
        }
    }
}