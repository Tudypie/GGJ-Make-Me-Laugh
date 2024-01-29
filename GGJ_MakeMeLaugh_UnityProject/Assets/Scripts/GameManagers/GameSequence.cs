using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using GameJam.Audio;
using UnityEngine.Events;
using FMODUnity;
using GameJam.Input;

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
        public bool isJoke;
        public float laughDelay;
        public bool shutDownMonitor;
        public bool playAISound;
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
        public float subtitlesDelay;
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
    [SerializeField] private TMP_Text monitorText;
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

        if (InputManager.INPUT.Player.NextMessage.IsPressed()) NextMessage();

        if (InputManager.INPUT.Player.NextSequence.IsPressed()) NextSequence();
    }

    private void HideSubtitles() => subtitlesText.text = "";

    private void TriggerMessageEvent() => sequences[currentSequenceNum].messages[currentMessageNum].eventToTrigger?.Invoke();

    private void PlayLaugh() => AudioManager.Instance.PlayAudio(FMODEvents.Instance.playerLaugh);

    private void ShutDownMonitor()
    {
        AudioManager.Instance.PlayAudio(FMODEvents.Instance.monitorOff, monitorText.gameObject.transform.position);
        monitorText.text = "";
    }

    public void StartGame() => StartCoroutine(Sequence());

    public void NextMessage(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();

        currentMessageNum += increment;
        if (sequences[currentSequenceNum].messages[currentMessageNum].author == author.Narrator) audioPlayer.Stop();
        StartCoroutine(Sequence());
    }

    public void NextSequence(int increment = 1)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();

        currentMessageNum = 0;
        currentSequenceNum += increment;
        if (sequences[currentSequenceNum].messages[currentMessageNum].author == author.Narrator) audioPlayer.Stop();
        StartCoroutine(Sequence());
    }

    public void ChangeSequence(string sequenceName)
    {
        StopAllCoroutines();
        CancelInvoke();
        HideSubtitles();

        currentMessageNum = 0;
        for (int i = 0; i < sequences.Length; i++)
        {
            if (sequences[i].name == sequenceName) currentSequenceNum = i;
        }
        if (sequences[currentSequenceNum].messages[currentMessageNum].author == author.Narrator) audioPlayer.Stop();
        StartCoroutine(Sequence());
    }

    public void Die(int deathIndex)
    {
        StopAllCoroutines();
        CancelInvoke();
        audioPlayer.Stop();
        currentMessageNum = 0;
        SavingManager.Instance.LoadGame();
        StartCoroutine(PlayerDie(deathIndex));
    }

    IEnumerator PlayerDie(int deathIndex)
    {
        blackScreen.SetActive(true);
        AudioManager.Instance.PlayAudio(deaths[deathIndex].audioEvent);
        yield return new WaitForSeconds(deaths[deathIndex].subtitlesDelay);
        subtitlesText.text = deaths[deathIndex].message;
        yield return new WaitForSeconds(deaths[deathIndex].duration);
        blackScreen.SetActive(false);
        subtitlesText.text = "";
        if(currentSequenceNum == 0) StartCoroutine(Sequence());
    }

    IEnumerator ShowMonitorText(string fullText)
    {
        string currentText;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            AudioManager.Instance.PlayAudio(FMODEvents.Instance.monitorBeeps, monitorText.gameObject.transform.position);
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
                    StartCoroutine(ShowMonitorText(sequences[currentSequenceNum].messages[currentMessageNum].text));
                    if (sequences[currentSequenceNum].messages[currentMessageNum].isJoke)
                    {
                        Invoke(nameof(PlayLaugh), sequences[currentSequenceNum].messages[currentMessageNum].laughDelay);
                    }
<<<<<<< Updated upstream
                    if (sequences[currentSequenceNum].messages[currentMessageNum].shutDownMonitor)
                    {
                        Invoke(nameof(ShutDownMonitor), sequences[currentSequenceNum].messages[currentMessageNum].duration);
                    }
                    if(sequences[currentSequenceNum].messages[currentMessageNum].playAISound)
                    {
                        audioPlayer.Stop();
                        audioPlayer.EventReference = sequences[currentSequenceNum].messages[currentMessageNum].audioEvent;
                        audioPlayer.Play();
                    }
=======
>>>>>>> Stashed changes
                } 
                else
                {
                    subtitlesText.text = sequences[currentSequenceNum].messages[currentMessageNum].text;
                    audioPlayer.Stop();
                    audioPlayer.EventReference = sequences[currentSequenceNum].messages[currentMessageNum].audioEvent;
                    audioPlayer.Play();
                    //StartCoroutine(ShowSubtitlesText(sequences[currentSequenceNum].messages[currentMessageNum].text));
                }

                Invoke(nameof(TriggerMessageEvent), sequences[currentSequenceNum].messages[currentMessageNum].triggerDelay);

                if (sequences[currentSequenceNum].messages[currentMessageNum].shutDownMonitor)
                {
                    Invoke(nameof(ShutDownMonitor), sequences[currentSequenceNum].messages[currentMessageNum].duration);
                }

                if (sequences[currentSequenceNum].messages[currentMessageNum].autoNext)
                {
                    Debug.Log("waiting for message to end");
                    yield return new WaitForSeconds(sequences[currentSequenceNum].messages[currentMessageNum].duration);
                    HideSubtitles();
                    yield return new WaitForSeconds(1f);
                    currentMessageNum++;
                    Debug.Log("currentMessageNum++");
                }
                else
                {
                    Debug.Log("waiting for next message");
                    Invoke(nameof(HideSubtitles), sequences[currentSequenceNum].messages[currentMessageNum].duration);
                    yield return new WaitForSeconds(99999f);
                }
            }

            if (sequences[currentSequenceNum].autoNext) currentSequenceNum++;
            currentMessageNum = 0;
            Debug.Log("end of sequence");
        }
    }
}
