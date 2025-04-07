using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public void RecieveQuest(Quest quest)
    {
        bool questAlreadyReceived = false;

        foreach (Quest existingQuest in questList)
        {
            if (existingQuest.title == quest.title) 
            {
                questAlreadyReceived = true;
                break;
            }
        }

        if (!questAlreadyReceived && !quest.isRecieved)
        {
            questList.Add(quest);
            quest.Recieve();
        }
    }
    public void CompleteQuest(Quest quest)
    {
        if (questList.Contains(quest))
        {
            quest.CheckCompletion();
            if (quest.isCompleted)
            {
                questList.Remove(quest);
            }
        }
    }
}
