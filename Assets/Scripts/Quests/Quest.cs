using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestBase Base { get; private set; }
    public QuestStatus Status { get; private set; }

    public Quest(QuestBase _base)
    {
        Base = _base;
    }

    public void StartQuest()
    {
        Status = QuestStatus.Started;
        GameManager.Instance.dialogueManager.StartDialogue(Base.StartDialogue);
    }

    public void CompleteQuest()
    {
        Status = QuestStatus.Completed;
        GameManager.Instance.dialogueManager.StartDialogue(Base.CompletedDialogue);

        
        if (Base.RequiredItem != null)
        {
            // Remove item from the inventory.
        }
    }
}

public enum QuestStatus { None, Started, Completed }