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
        SceneManager.LoadScene("Game");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
