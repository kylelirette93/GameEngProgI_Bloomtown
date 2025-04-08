using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public List<GoalInstance> activeGoals = new();
    public HashSet<string> pickedUpItems = new();
    public Interactable questGiver { get; set; }

    /// <summary>
    /// Handles the interaction with the interactable object.
    /// </summary>
    /// <param name="interactable">The interactable being passed.</param>
    public void HandleInteraction(Interactable interactable)
    {
        foreach (var goal in activeGoals)
        {
            switch (goal.data.Objective)
            {
                case Goal.Type.Collected:
                    if (goal.data.requiredItem == interactable.itemData)
                    {
                        goal.isCompleted = true;
                    }
                    break;
                case Goal.Type.SpokenTo:
                    if (goal.data.description == interactable.name)
                    {
                        goal.isCompleted = true;
                    }
                    break;
                case Goal.Type.Delivered:
                    if (goal.data.requiredItem == interactable.itemData)
                    {
                        goal.isCompleted = true;
                    }
                    break;
                default:
                    Debug.Log("No goal found.");
                    break;
            }
        }
    }

    public void RecieveQuest(Quest quest)
    {
        if (quest == null)
        {
            Debug.LogWarning("No quest assigned to give!");
            return;
        }

        if (activeQuests.Contains(quest))
        {
            Debug.Log($"Quest '{quest.questName}' is already active.");
            return;
        }

        if (quest.goals == null) return;

        Debug.Log($"Receiving quest: {quest.questName}");

        activeQuests.Add(quest);

        foreach (var goal in quest.goals)
        {
            if (goal != null)
            {
                var instance = new GoalInstance(goal);
                activeGoals.Add(instance);
                Debug.Log($"Added goal: {goal.description}");
            }
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (quest == null) return;
        if (!activeQuests.Contains(quest))
        {
            Debug.LogWarning($"Quest '{quest.questName}' is not active.");
            return;
        }
        activeQuests.Remove(quest);
        foreach (var goal in quest.goals)
        {
            if (goal != null)
            {
                var instance = new GoalInstance(goal);
                activeGoals.Remove(instance);
                Debug.Log($"Removed goal: {goal.description}");
            }
        }
    }

    public bool HasPickedUp(string itemId)
    {
        return pickedUpItems.Contains(itemId);
    }

    public void MarkPickedUp(string itemId)
    {
        pickedUpItems.Add(itemId);
    }
}