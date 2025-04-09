using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();
    public int CurrentQuestIndex = 0;

    public void AddQuest(string Name)
    {
        Quest quest = questList.Find(q => q.Name == Name);
        Debug.Log($"Trying to find quest with name {Name} found? {quest != null}");
        if (!activeQuests.Contains(quest) && quest.canStart && !quest.isStarted && CurrentQuestIndex >= quest.requiredIndex)
        {
            activeQuests.Add(quest);
        }
    }

    public void RemoveQuest(string Name)
    {
        Quest quest = questList.Find(q => q.Name == Name);
        questList.Remove(quest);
        activeQuests.Remove(quest);
    }

    public Quest FindQuest(string Name)
    {
        Quest quest = questList.Find(q => q.Name == Name);
        if (activeQuests.Contains(quest))
        {
            return quest;
        }
        else
        {
            return quest;
        }
    }
}
