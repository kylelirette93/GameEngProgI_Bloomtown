using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea] public string description;
    public List<Goal> goals = new();
    public bool isRepeatable = false;
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
}

[System.Serializable]
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

    public QuestInstance(Quest quest)
    {
        data = quest;
    }
}