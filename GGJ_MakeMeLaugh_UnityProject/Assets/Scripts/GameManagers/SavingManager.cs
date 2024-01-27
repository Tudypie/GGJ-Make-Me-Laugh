using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    public string savedSceneName;
    public int savedGameSequence;
    public Vector3 savedPlayerPosition;
    private Transform player;

    public static SavingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player = PlayerManager.Instance.gameObject.transform;
        savedPlayerPosition = player.position;
    }

    public void SaveGame(string currentScene)
    {
        savedSceneName = currentScene;
        savedGameSequence = GameSequence.Instance.currentSequenceNum;
        savedPlayerPosition = player.position;
    }

    public void LoadGame()
    {
        SceneManagement.Instance.LoadScene(savedSceneName);
        GameSequence.Instance.currentSequenceNum = savedGameSequence;
        player.position = savedPlayerPosition;
    }
}
