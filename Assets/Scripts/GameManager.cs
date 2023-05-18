using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Lives { get; private set; }
    public int Coins { get; private set; }

    public event Action<int> OnLivesChanged;
    public event Action<int> OnCoinsChanged;

    private int currentLevelIndex;

    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        ResetLivesAndCoins();
    }
    
    internal void KillPlayer() 
    {
        Lives--;

        if(OnLivesChanged !=  null)
            OnLivesChanged(Lives);

        if (Lives <= 0)
            RestartGame();
        else
            SendPlayerToCheckPoint();
    }

    private void SendPlayerToCheckPoint()
    {
        var checkpointManager = FindObjectOfType<CheckPointManager>();
        var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
        var player = FindObjectOfType<PlayerMovementController>();

        player.transform.position = checkpoint.transform.position;
    }

    internal void AddCoin()
    {
        Coins++;

        if (OnCoinsChanged != null)
            OnCoinsChanged(Coins);
    }

    public void MoveToNextLevel() 
    {
        currentLevelIndex++;
        
        if (currentLevelIndex >= SceneManager.sceneCountInBuildSettings)
            currentLevelIndex = 0;

        SceneManager.LoadScene(currentLevelIndex);
    }

    private void RestartGame()
    {
        ResetLivesAndCoins();
        SceneManager.LoadScene(currentLevelIndex);
    }

    private void ResetLivesAndCoins()
    {
        Lives = 3;
        Coins = 0;

        if (OnCoinsChanged != null)
            OnCoinsChanged(Coins);
    }
}
