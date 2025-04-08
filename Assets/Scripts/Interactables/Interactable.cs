using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Info,
    Dialogue,
    QuestGiver
}
public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    public InteractableType type;

    [Header("Quest Giver Settings")]
    [SerializeField] private Quest questToGive;

    public ItemData itemData;
    Inventory inventory;
    [SerializeField] string itemId;

    [Header("Dialogue Settings")]
    [TextArea] public string[] sentences;
    [TextArea] public string[] completedSentences;

    QuestManager questManager;
    DialogueManager dialogueManager;


    private bool dialogueStarted = false;

    public InteractableType InteractionType => type;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        questManager = GameManager.Instance.questManager;
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
            case InteractableType.QuestGiver:
                if (questManager != null && questToGive != null)
                {
                    questManager.RecieveQuest(questToGive);
                }
                else
                {
                    Debug.Log("No quest assigned to give!" + questToGive.questName); // Debug log for no quest assigned.
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
        questManager?.MarkPickedUp(itemId);

        questManager?.HandleInteraction(this);

        foreach (var goal in questManager.activeGoals)
        {
            if (goal.data.requiredItem == itemData)
            {
                goal.isCompleted = true;
                Debug.Log($"Goal '{goal.data.description}' completed.");
            }
        }

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
