using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Name;
    public string Description;
    public string itemName;
    public int requiredIndex;
    public bool canStart;
    public bool isStarted = false;
    public bool isCompleted = false;
    public string[] notStartedDialogue;
    public string[] inProgressDialogue;
    public string[] onCompletionDialogue;
    public string[] onReturnDialogue;

    public void StartQuest()
    {
        if (requiredIndex == GameManager.Instance.questManager.CurrentQuestIndex)
        {
            canStart = true;
        }
        else
        {
            canStart = false;
        }
    }

    public void CheckIfCanStart()
    {
        if (requiredIndex == GameManager.Instance.questManager.CurrentQuestIndex)
        {
            canStart = true;
        }
        else
        {
            canStart = false;
        }
    }
    public void CompleteQuest()
    {
        isCompleted = true;
        GameManager.Instance.questManager.CurrentQuestIndex++;
    }
}