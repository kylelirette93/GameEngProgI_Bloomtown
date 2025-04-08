using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea] public string description;
    public List<Goal> goals = new();
    public bool isCompleted = false;

    public int currentGoalIndex = 0;

    [Header("Dialogue")]
    [TextArea] public string[] onAcceptDialogue;
    [TextArea] public string[] onCompleteDialogue;
}

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Goal")]
public class Goal : ScriptableObject
{
    public enum Type { Nothing, SpokenTo, Collected, Delivered }
    public Type Objective;
    public string description;
    public bool isCompleted = false;
    public ItemData requiredItem;

    [Header("Dialogue")]
    [TextArea] public string[] notStartedDialogue;
    [TextArea] public string[] inProgressDialogue;
    [TextArea] public string[] completedDialogue;
}

public class GoalInstance
{
    public Goal data;
    public bool isCompleted = false;

    public GoalInstance(Goal goal)
    {
        data = goal;
    }
}

[System.Serializable]
public class QuestInstance
{
    public Quest data;
    public bool isCompleted = false;
    public List<GoalInstance> goalInstances = new List<GoalInstance>();

    public QuestInstance(Quest quest)
    {
        data = quest;
        goalInstances = quest.goals.ConvertAll(goal => new GoalInstance(goal));
    }
}