using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{

    public enum GameState
    {
        MainMenu_State, // The game is at the main menu
        Options_State, // The game is at options menu
        Quest_State,     // The game is at the quest menu
        Gameplay_State,   // The game is actively being played
        Paused_State      // The game is paused
    }

    // Property to store the current game state
    public GameState currentState { get; private set; }

    public GameState previousState { get; private set; }

    private void Start()
    {
        // Set the initial state of the game to Main Menu when the game starts
        ChangeState(GameState.MainMenu_State);
    }

    // Method to change the current game state
    public void ChangeState(GameState newState)
    {
        // Exit the previous state before entering the new state
        ExitState(currentState);
        previousState = currentState;
        currentState = newState;
        EnterState(currentState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Gameplay_State)
            {
                ChangeState(GameState.Paused_State);  // Switch to Paused State
            }
            else if (currentState == GameState.Paused_State)
            {
                ChangeState(GameState.Gameplay_State);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentState == GameState.Gameplay_State)
            {
                ChangeState(GameState.Quest_State);
            }
            else if (currentState == GameState.Quest_State)
            {
                ChangeState(GameState.Gameplay_State);
            }
        }
    }

    // Handles any specific actions that need to occur when switching to a new state
    private void ExitState(GameState state)
    {
        previousState = state;
        switch (state)
        {
            case GameState.MainMenu_State:
                GameManager.Instance.UIManager.DisableAllMenuUI();
                Debug.Log("Exited MainMenu State");
                break;
            case GameState.Options_State:
                GameManager.Instance.UIManager.DisableAllMenuUI();
                Debug.Log("Exited Options State");
                break;
            case GameState.Quest_State:
                GameManager.Instance.UIManager.DisableAllMenuUI();
                Debug.Log("Exited Quest State");
                break;
            case GameState.Gameplay_State:
                GameManager.Instance.UIManager.DisableAllMenuUI();
                Debug.Log("Exited Gameplay State");
                break;

            case GameState.Paused_State:
                GameManager.Instance.UIManager.DisableAllMenuUI();
                Debug.Log("Exited Paused State");
                break;
        }
    }

    private void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu_State:
                EnableCursor();
                GameManager.Instance.UIManager.EnableMainMenuUI();
                Time.timeScale = 0;  // Stop gameplay when entering Main Menu
                Debug.Log("Entered MainMenu State");
                break;
            case GameState.Options_State:
                EnableCursor();
                GameManager.Instance.UIManager.EnableOptionsUI();
                Time.timeScale = 0;
                break;
            case GameState.Quest_State:
                EnableCursor();
                GameManager.Instance.UIManager.EnableQuestUI();
                Time.timeScale = 0;
                break;
            case GameState.Gameplay_State:
                GameManager.Instance.SoundManager.PlaySoundByName("gameplay");
                DisableCursor();
                GameManager.Instance.UIManager.EnableGameplayMenuUI();
                Time.timeScale = 1;  // Resume gameplay
                Debug.Log("Entered Gameplay State");
                break;

            case GameState.Paused_State:
                EnableCursor();
                GameManager.Instance.UIManager.EnablePauseMenuUI();
                Time.timeScale = 0;  // Pause gameplay
                Debug.Log("Entered Paused State");
                break;
        }
    }

    public void Play()
    {
        ChangeState(GameState.Gameplay_State);
        GameManager.Instance.levelManager.LoadScene("Scene01");
    }

    public void Back()
    {
        ChangeState(previousState);
    }
    public void Resume()
    {
        ChangeState(GameState.Gameplay_State);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void DisplayQuest()
    {
        ChangeState(GameState.Quest_State);
    }

    public void Options()
    {
        ChangeState(GameState.Options_State);
    }

    public void Pause()
    {
        ChangeState(GameState.Paused_State);
    }

    public void ChangeToMenuState()
    {
        GameManager.Instance.levelManager.LoadScene("MainMenu");
        ChangeState(GameState.MainMenu_State);
    }
    public void EnableCursor()
    {
        Cursor.visible = true;
    }
    public void DisableCursor()
    {
        Cursor.visible = false;
    }
}