using UnityEngine;

public enum InteractableType
{
    Nothing,
    Pickup,
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
            case InteractableType.Pickup:
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


    public void Pickup()
    {
        if (inventory == null || itemData == null) return;
        inventory.AddItem(itemData, 1);
        gameObject.SetActive(false);
        
    }

    public void Dialogue()
    {
        switch (questType)
        {
            case QuestType.PickFlower:
                if (questManager.pickFlowerQuestStatus == QuestManager.PickFlowerQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.pickFlowerQuestStatus = QuestManager.PickFlowerQuestStatus.InProgress;
                }
                else if (questManager.pickFlowerQuestStatus == QuestManager.PickFlowerQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.pickFlowerQuestStatus == QuestManager.PickFlowerQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    inventory.RemoveAll();
                    questManager.pickFlowerQuestStatus = QuestManager.PickFlowerQuestStatus.After;
                }
                else if (questManager.pickFlowerQuestStatus == QuestManager.PickFlowerQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.PickMushrooms:
                if (questManager.pickMushroomsQuestStatus == QuestManager.PickMushroomsQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.pickMushroomsQuestStatus = QuestManager.PickMushroomsQuestStatus.InProgress;
                }
                else if (questManager.pickMushroomsQuestStatus == QuestManager.PickMushroomsQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.pickMushroomsQuestStatus == QuestManager.PickMushroomsQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    inventory.RemoveAll();
                    questManager.pickMushroomsQuestStatus = QuestManager.PickMushroomsQuestStatus.After;
                }
                else if (questManager.pickMushroomsQuestStatus == QuestManager.PickMushroomsQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
                case QuestType.TalkToElders:
                if (questManager.talkToEldersQuestStatus == QuestManager.TalkToEldersQuestStatus.NotStarted)
                {
                    sentences = notStartedDialogue;
                    questManager.talkToEldersQuestStatus = QuestManager.TalkToEldersQuestStatus.InProgress;
                }
                else if (questManager.talkToEldersQuestStatus == QuestManager.TalkToEldersQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.talkToEldersQuestStatus == QuestManager.TalkToEldersQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    questManager.talkToEldersQuestStatus = QuestManager.TalkToEldersQuestStatus.After;
                }
                else if (questManager.talkToEldersQuestStatus == QuestManager.TalkToEldersQuestStatus.After)
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
