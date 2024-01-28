using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingManager : MonoBehaviour
{
    public string savedSceneName;
    public int savedGameSequence;
    public Vector3 savedPlayerPosition;
    public Quaternion savedPlayerRotation;
    private Transform player;

    public static SavingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = PlayerManager.Instance.gameObject.transform;
        SaveGame("WhiteRoom");
    }

    public void SaveGame(string currentScene = "")
    {
        savedSceneName = currentScene;
        savedGameSequence = GameSequence.Instance.currentSequenceNum;
        savedPlayerPosition = player.position;
        savedPlayerRotation = player.rotation;
    }

    public void LoadGame()
    {
        if (savedSceneName != "")
        {
            SceneManagement.Instance.UnloadScene(savedSceneName);
            SceneManagement.Instance.LoadScene(savedSceneName);
        }

        GameSequence.Instance.currentSequenceNum = savedGameSequence;
        player.position = savedPlayerPosition;
        player.rotation = savedPlayerRotation;
    }
}
