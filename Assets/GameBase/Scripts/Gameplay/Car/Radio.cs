using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Radio : MonoBehaviour
{
    [SerializeField] private List<AudioClip> songs;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TMP_Text songTitleText; // ������ �� UI Text
    [SerializeField] private float scrollSpeed = 50f; // �������� ��������� ������
    [SerializeField] private float displayDelay = 2f; // �������� ����� ������� ���������

    private int currentSongIndex = 0;
    private Coroutine scrollCoroutine;
    private string fullSongName;

    private bool _rep = true;
    private bool _inter = true;
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
            Debug.LogWarning("�� �������� UI Text ��� ����������� �������� �����!");
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

        // ������������� ���������� ��������, ���� ��� ����
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }

        // ������� ���������� ������ �������� ��� ���������
        songTitleText.text = fullSongName;

        // ��������� ��������� ����� ��������
        scrollCoroutine = StartCoroutine(ScrollSongTitle());
    }

    private IEnumerator ScrollSongTitle()
    {
        // ���� ����� ������� ���������
        yield return new WaitForSeconds(displayDelay);

        // ���� ����� ����������, �� ������������
        if (IsTextFitting(fullSongName))
        {
            yield break;
        }

        // ��������� ������� � ����� ��� ������� "�����" ������
        string scrollingText = fullSongName + "     ";
        float textPosition = 0;

        while (true)
        {
            // ��������� ������� ����� ������
            int startChar = Mathf.FloorToInt(textPosition);
            int visibleChars = Mathf.CeilToInt(GetTextWidth(fullSongName) / songTitleText.fontSize);

            string visibleText = "";
            for (int i = 0; i < visibleChars; i++)
            {
                int currentCharPos = (startChar + i) % scrollingText.Length;
                visibleText += scrollingText[currentCharPos];
            }

            songTitleText.text = visibleText;

            // ������� �������
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
        // ������� �������� - ���� �������� ������ N ��������, �� ������������
        // ����� �������� �� ����� ������ �������� � ������ ������� UI ��������
        return text.Length <= 10; // ������������ ��������, ��������� ��� ��� UI
    }

    private float GetTextWidth(string text)
    {
        // ��������� ������ ������ ������
        // ��� ������� ������� ����� ������������ TextGenerator
        return text.Length * songTitleText.fontSize * 0.6f;
    }

    private void CheckForSpecificSongEvents(AudioClip currentSong)
    {
        if (currentSong.name == "Now_Play_No_Name_Techno_Remix_by_akxmo")
        {
            if (_rep)
            {
                _rep = false;
                StartCoroutine(StartDialogWithDelay(5f, "rep"));
            }
        }
        if (currentSong.name == "Now_Play_Internatsional_Techno_Remix_by_akxmo")
        {
            if (_inter)
            {
                _inter = false;
                StartCoroutine(StartDialogWithDelay(5f, "internatsional"));
            }
        }
    }
    private IEnumerator StartDialogWithDelay(float delay, string text)
    {
        yield return new WaitForSeconds(delay);
        Dialogs.Scripts.DialogManager.instance.StartDialog(text);
    }
}