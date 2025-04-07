using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public void RecieveQuest(Quest quest) 
    {
        if (!questList.Contains(quest))
        {
            questList.Add(quest);
            quest.Recieve();
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (questList.Contains(quest))
        {
            quest.Complete();
            questList.Remove(quest);
        }
    }
}
