using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingManager : MonoBehaviour
{
    public string savedSceneName;
    public int savedGameSequence;
    public int savedSequenceMessage;
    public Transform player;
    public GameObject playerPrefab;
    public Vector3 savedPlayerPosition;
    public Quaternion savedPlayerRotation;

    public static SavingManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //player = PlayerManager.Instance.gameObject.transform;
        SaveGame("WhiteRoom");
    }

    public void SaveGame(string currentScene = "")
    {
        savedSceneName = currentScene;
        savedGameSequence = GameSequence.Instance.currentSequenceNum;
        savedSequenceMessage = GameSequence.Instance.currentMessageNum;
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
        GameSequence.Instance.currentMessageNum = savedSequenceMessage;
        GameObject newPlayer = Instantiate(playerPrefab, savedPlayerPosition, savedPlayerRotation);
        Destroy(player.gameObject);
        player = newPlayer.transform;
        //PlayerManager.Instance.LoadSavedPosition();
    }
}
