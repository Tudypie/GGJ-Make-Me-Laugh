using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GameJam.Audio;
using UnityEngine.Events;
using FMODUnity;
using static Autodesk.Fbx.FbxAnimCurveDef;

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
        public EventReference audioEvent;
        public UnityEvent eventToTrigger;
        public float triggerDelay;
    }

    [Serializable]
    public struct sequence
    {
        public string name;
        public bool autoNext;
        public message[] messages;
    }

    [Serializable]
    public struct death
    {
        [TextArea(3, 7)]
        public string message;
        public float duration;
        public EventReference audioEvent;
    }

    [Header("Debug")]
    public int currentSequenceNum = 0;
    public int currentMessageNum = 0;
    [Space]
    [Header("References")]
    [SerializeField] private TMP_Text subtitlesText;
    [SerializeField] private GameObject blackScreen;
    private TMP_Text monitorText;
    [SerializeField] private float typewritterEffectDelay = 0.15f;
    [Space]
    public death[] deaths;
    [Space]
    [SerializeField] private sequence[] sequences;

    private StudioEventEmitter audioPlayer;

    public static GameSequence Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioPlayer = GetComponent<StudioEventEmitter>();
    }

    private void Start()
    {
        currentSequenceNum = 0;
        StartCoroutine(Sequence());
    }

    private void Update()
    {
        monitorText = GameObject.FindGameObjectWithTag("MonitorText").gameObject.GetComponent<TMP_Text>();
    }

    private void HideSubtitles() => subtitlesText.text = "";

    private void TriggerMessageEvent() => sequences[currentSequenceNum].messages[currentMessageNum].eventToTrigger?.Invoke();

    public void NextMessage(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();
        audioPlayer.Stop();
        currentMessageNum += increment;
        StartCoroutine(Sequence());
    }

    public void NextSequence(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();
        audioPlayer.Stop();

        currentMessageNum = 0;
        currentSequenceNum += increment;
        StartCoroutine(Sequence());
    }

    public void ChangeSequence(string sequenceName)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();
        audioPlayer.Stop();

        currentMessageNum = 0;
        for (int i = 0; i < sequences.Length; i++)
        {
            if (sequences[i].name == sequenceName) currentSequenceNum = i;
        }
        StartCoroutine(Sequence());
    }

    public void Die(int deathIndex)
    {
        currentMessageNum = 0;
        StopAllCoroutines();
        StartCoroutine(PlayerDie(deathIndex));
    }

    IEnumerator PlayerDie(int deathIndex)
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        subtitlesText.text = deaths[deathIndex].message;
        AudioManager.Instance.PlayAudio(deaths[deathIndex].audioEvent);
        yield return new WaitForSeconds(deaths[deathIndex].duration);
        blackScreen.SetActive(false);
        SavingManager.Instance.LoadGame();
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
                    audioPlayer.Stop();
                    audioPlayer.EventReference = sequences[currentSequenceNum].messages[currentMessageNum].audioEvent;
                    audioPlayer.Play();
                    //StartCoroutine(ShowSubtitlesText(sequences[currentSequenceNum].messages[currentMessageNum].text));
                    //AudioManager.Instance.PlayAudio(sequences[currentSequenceNum].messages[currentMessageNum].audioClip);
                }

                Invoke(nameof(TriggerMessageEvent), sequences[currentSequenceNum].messages[currentMessageNum].triggerDelay);
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
