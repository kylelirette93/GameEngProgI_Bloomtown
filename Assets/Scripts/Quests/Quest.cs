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
    public int goalsToComplete;
    public QuestStatus CurrentStatus;

    public void Recieve()
    {
        isRecieved = true;
        CurrentStatus = QuestStatus.Started;
    }

    public void Complete()
    {
        foreach (Goal goal in questGoals)
        {
            if (goal.goalIndex == goalsToComplete)
            {
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
    public int goalIndex = 0;

    public void Complete()
    {
        isCompleted = true;
        goalIndex++;
    }
}