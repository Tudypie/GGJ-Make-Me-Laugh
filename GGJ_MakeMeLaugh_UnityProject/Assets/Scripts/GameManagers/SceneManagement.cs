using GameJam.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        SceneManager.LoadScene("WhiteRoom", LoadSceneMode.Additive);
        SceneManager.LoadScene("FirstMonitorRoom", LoadSceneMode.Additive);
    }

    public void RestartScene()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void RestartGame()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        AudioManager.Instance.PlayAudio(FMODEvents.Instance.endGameDeath);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Game");
        SceneManager.LoadSceneAsync("WhiteRoom", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("FirstMonitorRoom", LoadSceneMode.Additive);
    }
}
