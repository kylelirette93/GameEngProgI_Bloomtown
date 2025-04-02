using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference all managers for access and persist across scenes.
    public static GameManager Instance;
    public GameStateManager GameStateManager;
    public UIManager UIManager;
    public LevelManager levelManager;
    public QuestManager questManager;
    public SoundManager SoundManager;
    public DialogueManager dialogueManager;
    public PlayerMovement playerController;

    private void Awake()
    {
        // Singleton pattern.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}