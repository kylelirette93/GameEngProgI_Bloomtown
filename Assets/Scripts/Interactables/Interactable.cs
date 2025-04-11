using UnityEditor;
using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
    Take,
    Info,
    Dialogue,
    Attack
}
public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Interactable Settings")]
    public InteractableType type;

    public ItemData itemData;
    public ItemData questReward;
    Inventory inventory;
    [SerializeField] string itemId;

    [Header("Dialogue Settings")]
    [TextArea] public string[] sentences;
    [TextArea] public string[] notStartedDialogue;
    [TextArea] public string[] inPogressDialogue;
    [TextArea] public string[] completionDialogue;
    [TextArea] public string[] afterDialogue;
    DialogueManager dialogueManager;
    private bool dialogueStarted = false;


    QuestManager questManager;
    public QuestType questType;
    public bool canPickup = false;
    public bool canAttack = false;

    public InteractableType InteractionType => type;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        dialogueManager = GameManager.Instance.dialogueManager.GetComponent<DialogueManager>();
        questManager = GameManager.Instance.questManager.GetComponent<QuestManager>();
        
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
            case InteractableType.Take:
                Take();
                break;
            case InteractableType.Pickup:
                // Taking things FROM an object, such as tree etc.
                Pickup();
                break;
            case InteractableType.Info:
                Info();
                break;
            case InteractableType.Dialogue:
                // If dialogue has not started, start it, otherwise display the next sentence.
                Dialogue();
                break;
            case InteractableType.Attack:
                Enemy enemy = GetComponent<Enemy>();
                enemy.TakeDamage(1);
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


    public void Take()
    {
        if (inventory == null || itemData == null) return;
        inventory.AddItem(itemData, 1);
        gameObject.SetActive(false);
        
    }

    public void Pickup()
    {
        if (inventory == null || itemData == null) return;
        inventory.AddItem(itemData, 1);
    }

    public void Dialogue()
    {
        switch (questType)
        {
            case QuestType.TakeFlower:
                if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.takeFlowerQuestStatus = QuestManager.TakeFlowerQuestStatus.InProgress;
                }
                else if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    inventory.RemoveAll();
                    questManager.takeFlowerQuestStatus = QuestManager.TakeFlowerQuestStatus.After;
                }
                else if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.TakeMushrooms:
                if (questManager.takeMushroomsQuestStatus == QuestManager.TakeMushroomsQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.takeMushroomsQuestStatus = QuestManager.TakeMushroomsQuestStatus.InProgress;
                }
                else if (questManager.takeMushroomsQuestStatus == QuestManager.TakeMushroomsQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.takeMushroomsQuestStatus == QuestManager.TakeMushroomsQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    inventory.RemoveAll();
                    questManager.takeMushroomsQuestStatus = QuestManager.TakeMushroomsQuestStatus.After;
                }
                else if (questManager.takeMushroomsQuestStatus == QuestManager.TakeMushroomsQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
                case QuestType.PickApples:
                if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.pickApplesQuestStatus = QuestManager.PickApplesQuestStatus.InProgress;
                }
                else if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    inventory.RemoveAll();
                    questManager.pickApplesQuestStatus = QuestManager.PickApplesQuestStatus.After;
                }
                else if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
                case QuestType.HuntRabbits:
                if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.huntRabbitsQuestStatus = QuestManager.HuntRabbitsQuestStatus.InProgress;
                }
                else if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    questManager.huntRabbitsQuestStatus = QuestManager.HuntRabbitsQuestStatus.After;
                }
                else if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;

        }
        if (dialogueStarted)
        {
            // Display the next sentence.
            dialogueManager.DisplayNextSentence();
        }
        else
        {
            // Start the dialogue.
            dialogueManager.StartDialogue(sentences);
            dialogueStarted = true;
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
