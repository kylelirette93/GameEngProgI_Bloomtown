using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Info,
    Dialogue
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
    public bool isQuestDialogue;

    DialogueManager dialogueManager;

    // Static dictionary to store whether an interactable has been picked up.
    static Dictionary<string, bool> isPickedUp = new Dictionary<string, bool>();

    private bool dialogueStarted = false;

    public InteractableType InteractionType => type;

    
    void OnEnable()
    {
        if (isPickedUp.ContainsKey(itemId)) 
        {
            gameObject.SetActive(false);
        }
    }

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
                if (GameManager.Instance.questManager.quests[itemData.questIndex].isRecieved
                    && GameManager.Instance.questManager.quests[itemData.questIndex].requiredItem == itemData)
                {
                    Pickup();
                }
                else
                {
                    // If quest is not recieved yet, display info of the item instead.
                    Info();
                }
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
        if (inventory != null)
        {
            if (GameManager.Instance.questManager.quests[itemData.questIndex].isRecieved)
            {
                inventory.AddItem(itemData);
                GameManager.Instance.questManager.quests[itemData.questIndex].collected += 1;
                GameManager.Instance.questManager.QuestUI.UpdateQuestList();

                isPickedUp[itemId] = true;
                gameObject.SetActive(false);
            }
        }
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
