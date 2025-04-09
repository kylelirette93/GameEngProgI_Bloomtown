using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Info,
    Dialogue,
    NPC
}
public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    public InteractableType type;

    public ItemData itemData;
    Inventory inventory;
    [SerializeField] string itemId;

    [Header("Dialogue Settings")]
    [TextArea] public string[] sentences;
    [TextArea] public string[] completedSentences;
    DialogueManager dialogueManager;
    private bool dialogueStarted = false;

    [Header("Quest Settings")]
    public string quest;
    public string quest2;
    public string npcName;

    public InteractableType InteractionType => type;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        dialogueManager = GameManager.Instance.dialogueManager.GetComponent<DialogueManager>();
        
        // Subscribe to delegate.
        DialogueManager.OnDialogueEnded += DialogueEndedHandler;
    }
    public void Interact()
    {
        switch (type)
        {
            case InteractableType.Nothing:
                Nothing();
                break;
            case InteractableType.Pickup:
                // Check if pickup is required for a quest, if so display prompt.
                Pickup();
                break;
            case InteractableType.Info:
                Info();
                break;
            case InteractableType.Dialogue:
                // If dialogue has not started, start it, otherwise display the next sentence.
                if (!dialogueStarted)
                {
                    dialogueManager.StartDialogue(sentences);
                    dialogueStarted = true;
                }
                else                 
                {
                    dialogueManager.DisplayNextSentence();
                }
                break;
            case InteractableType.NPC:
                if (string.IsNullOrEmpty(quest)) quest = quest2;

                GameManager.Instance.questManager.AddQuest(quest);              
                if (GameManager.Instance.questManager.activeQuests.Count > 0)
                {
                    Quest associatedQuest = GameManager.Instance.questManager.FindQuest(quest);
                    Debug.Log("Quest it's looking for: " + quest);

                    if (!associatedQuest.isStarted && !associatedQuest.isCompleted)
                    {
                        GameManager.Instance.dialogueManager.StartDialogue(associatedQuest.notStartedDialogue);
                        // Toggle quest started state.
                        associatedQuest.StartQuest();
                        associatedQuest.isStarted = true;
                        // Add quest to the quest manager.
                        GameManager.Instance.questManager.AddQuest(quest);
                        if (associatedQuest.itemName == string.Empty)
                        {
                            GameManager.Instance.dialogueManager.StartDialogue(associatedQuest.inProgressDialogue);
                            associatedQuest.isCompleted = true;
                            associatedQuest.CompleteQuest();
                            GameManager.Instance.questManager.RemoveQuest(quest);
                            foreach (Quest quest in GameManager.Instance.questManager.questList)
                            {
                                quest.CheckIfCanStart();
                            }
                        }
                    }
                    else if (associatedQuest.isStarted && !associatedQuest.isCompleted)
                    {
                        GameManager.Instance.dialogueManager.StartDialogue(associatedQuest.inProgressDialogue);
                    }
                    else if (associatedQuest.isCompleted)
                    {
                        GameManager.Instance.dialogueManager.StartDialogue(associatedQuest.onCompletionDialogue);
                        GameManager.Instance.questManager.RemoveQuest(quest);
                        foreach (Quest quest in GameManager.Instance.questManager.questList)
                        {
                            quest.CheckIfCanStart();
                        }
                    }
                    else
                    {
                        GameManager.Instance.dialogueManager.StartDialogue(associatedQuest.onReturnDialogue);
                    }
                }
                     
                break;
            default:
                Nothing();
                break;
        }    
    }

    public void Nothing()
    {
        Debug.Log("Interaction type not defined.");
    }


    public void Pickup()
    {
        if (inventory == null || itemData == null) return;
      
        inventory.AddItem(itemData);
        inventory.CheckForQuestItem();

        gameObject.SetActive(false);
    }

    public void Info()
    {
        // Set the floating text above player's head.
        GameManager.Instance.UIManager.SetInteractionText(itemData.infoMessage);
    }

    private void DialogueEndedHandler()
    {
        dialogueStarted = false;
    }
}
