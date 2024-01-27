using FMODUnity;
using GameJam.Audio;
using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerDeath : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private TMP_Text subtitlesText;

    [Header("Death message")]
    [SerializeField] private string deathMessage;
    [SerializeField] private AudioClip messageAudio;

    public IEnumerator PlayerDieAndRespawn()
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        subtitlesText.text = deathMessage;
        AudioManager.Instance.PlayAudio(messageAudio);
        yield return new WaitForSeconds(messageAudio.length+2f);
        SavingManager.Instance.LoadGame();
        blackScreen.SetActive(false);
    }
}
