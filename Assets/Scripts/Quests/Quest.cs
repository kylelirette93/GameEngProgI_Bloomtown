using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { Started, InProgress, Completed }
[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public bool isRecieved = false;
    public bool isCompleted = false;
    Goal currentGoal;
    public List<Goal> questGoals;
    int goalsCompleted;
    public int goalsToComplete;
    public QuestStatus CurrentStatus;
    

    public void Recieve()
    {
        isRecieved = true;
        CurrentStatus = QuestStatus.Started;
    }

    public void CheckCompletion()
    {
        foreach (Goal goal in questGoals)
        {
            goal.CheckCompletion();

            if (goal.isCompleted) goalsCompleted++;

            if (goalsCompleted >= goalsToComplete)
            {
                // The quest is completed.
                isCompleted = true;
                CurrentStatus = QuestStatus.Completed;
            }
        }
    }
}

[System.Serializable]
public class Goal
{
    public string objective;
    public string description;
    public bool isCompleted = false;
    public int currentAmount;
    public int requiredAmount;
    public ItemData requiredItem;

    public void CheckCompletion()
    {
        if (currentAmount >= requiredAmount)
        {
            isCompleted = true;
        }
    }
}