using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Name;
    public string Description;
    public bool isStarted = false;
    public bool isCompleted = false;
    public string[] notStartedDialogue;
    public string[] inProgressDialogue;
    public string[] onCompletionDialogue;
    public string[] onReturnDialogue;

    public void StartQuest()
    {
        isStarted = true;
    }

    public void CompleteQuest()
    {
        isCompleted = true;
    }
}