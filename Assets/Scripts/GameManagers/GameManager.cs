using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Base singleton class used to control the game and  switch between game states.
/// </summary>
public enum GameState { Wait, Play, Win, Lose}
public class GameManager : MonoBehaviour
{
    public ObjectSpawner objectSpawner;
    public UIController uiController;
    public Player player;
    public InventoryManager inventoryManager;
    public static GameManager Instance { get; private set; }
    private GameState m_CurrentGameState;
    public GameState CurrentGameState {
        get
        {
            return m_CurrentGameState;
        }
        set
        {
            m_CurrentGameState = value;
            if (m_CurrentGameState == GameState.Win)
            {
                uiController.ShowWinUI();
                
                inventoryManager.SpawnRandomInventoryItem();
            }
            Debug.Log("Game State switched to: " + m_CurrentGameState);
        }
    }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        uiController.AddStartButtonListener(StartGame);
        uiController.AddRestartButtonListener(RestartGame);
        uiController.SetProgressBar(0, true);
    }

    private void Reset()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
        uiController = FindObjectOfType<UIController>();
        player = FindObjectOfType<Player>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartGame()
    {
        CurrentGameState = GameState.Play;
    }
}
