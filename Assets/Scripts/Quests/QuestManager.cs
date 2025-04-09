using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        if (!quests.Contains(quest))
        {
            Debug.Log("Quest added: " + quest.Name + " - " + quest.Description);
            quests.Add(quest);
        }
    }
}
