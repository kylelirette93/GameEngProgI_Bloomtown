using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnding : MonoBehaviour
{
   LevelManager levelManager;
   QuestManager questManager;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        levelManager = GameManager.Instance.levelManager;
        questManager = GameManager.Instance.questManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && questManager.CurrentQuestIndex == 4)
        {
            GameManager.Instance.GameStateManager.ChangeState(GameStateManager.GameState.End_State);
        }
    }
}
