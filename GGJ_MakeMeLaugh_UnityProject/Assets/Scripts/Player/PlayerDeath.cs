using FMODUnity;
using GameJam.Audio;
using GameJam.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerDeath : MonoBehaviour
{
    [Serializable]
    public struct death
    {
        [TextArea(3,7)]
        public string message;
        public float duration;
        public EventReference audioEvent;
    }

    [Header("UI")]
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TMP_Text subtitlesText;

    [Header("Death messages")]
    public death[] deaths;

    public IEnumerator PlayerDieAndRespawn(int deathIndex)
    {
        GameSequence.Instance.StopAllCoroutines();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        subtitlesText.text = deaths[deathIndex].message;
        AudioManager.Instance.PlayAudio(deaths[deathIndex].audioEvent);
        yield return new WaitForSeconds(deaths[deathIndex].duration);
        SavingManager.Instance.LoadGame();
        blackScreen.SetActive(false);
    }
}
