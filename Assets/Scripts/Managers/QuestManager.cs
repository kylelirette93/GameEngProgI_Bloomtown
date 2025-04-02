using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // List of quest objects.
    public List<Quest> quests = new List<Quest>();
    public QuestUI QuestUI;

    private void Start()
    {
        if (QuestUI == null)
        {
            QuestUI = FindObjectOfType<QuestUI>();
        }
    }


    public void CompleteQuest(string title)
    {
        // Search the list of quests, match the title and complete the quest.
        Quest quest = quests.Find(q => q.title == title);
        if (quest != null && !quest.isCompleted)
        {
            quest.CheckCompletion();
            // Temporarily display the quest is completed, for feedback.
            if (QuestUI != null && quest.isCompleted)
            {
                StartCoroutine(QuestUI.DisplayCompletion(quest, this));
            }
        }
    }

    public List<Quest> GetActiveQuests()
    {
        // Search through and return all quests that are not completed.
        return quests.FindAll(q => !q.isCompleted);
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
    }

}
