using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GameJam.Audio;
using UnityEngine.Events;

public class GameSequence : MonoBehaviour
{
    public enum author
    {
        Narrator,
        AI
    }

    [Serializable]
    public struct message
    {
        public author author;
        [TextArea(5, 10)]
        public string text;
        public float duration;
        public bool autoNext;
        public AudioClip audioClip;
        public UnityEvent eventToTrigger;
    }

    [Serializable]
    public struct sequence
    {
        public string name;
        public bool autoNext;
        public message[] messages;
    }

    [Header("Debug")]
    public int currentSequenceNum = 0;
    public int currentMessageNum = 0;
    [Space]
    [Header("References")]
    [SerializeField] private TMP_Text monitorText;
    [SerializeField] private TMP_Text subtitlesText;
    [SerializeField] private float typewritterEffectDelay = 0.15f;
    [Space]
    [Header("Sequences")]
    [SerializeField] private sequence[] sequences;

    public static GameSequence Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSequenceNum = PlayerPrefs.GetInt("sequence");
        StartCoroutine(Sequence());
    }

    private void HideSubtitles() => subtitlesText.text = "";

    public void NextMessage(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();
        AudioManager.Instance.audioSource.Stop();
        currentMessageNum += increment;
        StartCoroutine(Sequence());
    }

    public void NextSequence(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();
        AudioManager.Instance.audioSource.Stop();

        currentMessageNum = 0;
        currentSequenceNum += increment;
        StartCoroutine(Sequence());
    }

    public void ChangeSequence(string sequenceName)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();
        AudioManager.Instance.audioSource.Stop();

        currentMessageNum = 0;
        for (int i = 0; i < sequences.Length; i++)
        {
            if (sequences[i].name == sequenceName) currentSequenceNum = i;
        }
        StartCoroutine(Sequence());
    }

    IEnumerator ShowMonitorText(string fullText)
    {
        string currentText;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            monitorText.text = currentText;
            yield return new WaitForSeconds(typewritterEffectDelay);
        }
    }

    IEnumerator ShowSubtitlesText(string fullText)
    {
        string currentText;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            subtitlesText.text = currentText;
            yield return new WaitForSeconds(typewritterEffectDelay);
        }
    }

    IEnumerator Sequence()
    {
        while (currentSequenceNum < sequences.Length)
        {
            Debug.Log("new sequence");

            if (currentMessageNum == sequences[currentSequenceNum].messages.Length)
            {
                Debug.Log("waiting to start sequence");
                yield return new WaitForEndOfFrame();
            }

            while (currentMessageNum < sequences[currentSequenceNum].messages.Length)
            {
                if(sequences[currentSequenceNum].messages[currentMessageNum].author == author.AI)
                {
                    //monitorText.text = sequences[currentSequenceNum].messages[currentMessageNum].text;
                    StartCoroutine(ShowMonitorText(sequences[currentSequenceNum].messages[currentMessageNum].text));
                } 
                else
                {
                    subtitlesText.text = sequences[currentSequenceNum].messages[currentMessageNum].text;
                    //StartCoroutine(ShowSubtitlesText(sequences[currentSequenceNum].messages[currentMessageNum].text));
                    //AudioManager.Instance.PlayAudio(sequences[currentSequenceNum].messages[currentMessageNum].audioClip);
                }

                sequences[currentSequenceNum].messages[currentMessageNum].eventToTrigger?.Invoke();
                //Invoke(nameof(HideSubtitles), sequences[currentSequenceNum].messages[currentMessageNum].audioClip.length);

                if (sequences[currentSequenceNum].messages[currentMessageNum].autoNext)
                {
                    Debug.Log("waiting for message to end");
                    yield return new WaitForSeconds(sequences[currentSequenceNum].messages[currentMessageNum].duration - 1);
                    HideSubtitles();
                    yield return new WaitForSeconds(1f);
                    currentMessageNum++;
                    Debug.Log("currentMessageNum++");
                }
                else
                {
                    Debug.Log("waiting for next message");
                    Invoke(nameof(HideSubtitles), sequences[currentSequenceNum].messages[currentMessageNum].duration - 1);
                    yield return new WaitForSeconds(99999f);
                }
            }

            if (sequences[currentSequenceNum].autoNext) currentSequenceNum++;
            currentMessageNum = 0;
            Debug.Log("end of sequence");
        }
    }
}
