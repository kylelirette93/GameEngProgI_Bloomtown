using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum InteractableType
{
    Nothing,
    Pickup,
    Take,
    Info,
    Dialogue,
    Attack,
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
    [TextArea] public string[] cantStartDialogue;
    [TextArea] public string[] notStartedDialogue;
    [TextArea] public string[] inPogressDialogue;
    [TextArea] public string[] completionDialogue;
    [TextArea] public string[] afterDialogue;
    DialogueManager dialogueManager;
    private bool dialogueStarted = false;
    public string npcName;


    QuestManager questManager;
    public QuestType questType;
    public bool canPickup = false;
    public bool canAttack = false;
    public int requiredQuestIndex;

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
                    questManager.CurrentQuestIndex++;
                    questManager.takeMushroomsQuestStatus = QuestManager.TakeMushroomsQuestStatus.After;
                }
                else if (questManager.takeMushroomsQuestStatus == QuestManager.TakeMushroomsQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.PickApples:
                if (questManager.CurrentQuestIndex != requiredQuestIndex && questManager.pickApplesQuestStatus != QuestManager.PickApplesQuestStatus.After)
                {
                    sentences = cantStartDialogue;
                    questManager.pickApplesQuestStatus = QuestManager.PickApplesQuestStatus.CantStart;
                }
                if (questManager.CurrentQuestIndex == requiredQuestIndex && questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.CantStart)
                {
                    questManager.pickApplesQuestStatus = QuestManager.PickApplesQuestStatus.NotStarted;
                }
                if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.NotStarted && questManager.CurrentQuestIndex == requiredQuestIndex)
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
                    questManager.CurrentQuestIndex++;
                    questManager.pickApplesQuestStatus = QuestManager.PickApplesQuestStatus.After;
                }
                else if (questManager.pickApplesQuestStatus == QuestManager.PickApplesQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.TakeFlower:
                if (questManager.CurrentQuestIndex != requiredQuestIndex && questManager.takeFlowerQuestStatus != QuestManager.TakeFlowerQuestStatus.After)
                {
                    sentences = cantStartDialogue;
                    questManager.takeFlowerQuestStatus = QuestManager.TakeFlowerQuestStatus.CantStart;
                }
                if (questManager.CurrentQuestIndex == requiredQuestIndex && questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.CantStart)
                {
                    questManager.takeFlowerQuestStatus = QuestManager.TakeFlowerQuestStatus.NotStarted;
                }
                if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.NotStarted && questManager.CurrentQuestIndex == requiredQuestIndex)
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
                    questManager.CurrentQuestIndex++;
                    questManager.takeFlowerQuestStatus = QuestManager.TakeFlowerQuestStatus.After;
                }
                else if (questManager.takeFlowerQuestStatus == QuestManager.TakeFlowerQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.HuntRabbits:
                if (questManager.CurrentQuestIndex != requiredQuestIndex && questManager.huntRabbitsQuestStatus != QuestManager.HuntRabbitsQuestStatus.After)
                {
                    sentences = cantStartDialogue;
                    questManager.huntRabbitsQuestStatus = QuestManager.HuntRabbitsQuestStatus.CantStart;
                }
                if (questManager.CurrentQuestIndex == requiredQuestIndex && questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.CantStart)
                {
                    questManager.huntRabbitsQuestStatus = QuestManager.HuntRabbitsQuestStatus.NotStarted;
                }
                    if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.NotStarted && questManager.CurrentQuestIndex == requiredQuestIndex)
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
                    questManager.CurrentQuestIndex++;
                    questManager.huntRabbitsQuestStatus = QuestManager.HuntRabbitsQuestStatus.After;
                }
                else if (questManager.huntRabbitsQuestStatus == QuestManager.HuntRabbitsQuestStatus.After)
                {
                    sentences = afterDialogue;
                }
                break;
            case QuestType.ReturnToWizard:
                if (questManager.CurrentQuestIndex != requiredQuestIndex && questManager.returnToWizardQuestStatus != QuestManager.ReturnToWizardQuestStatus.After)
                {
                    sentences = cantStartDialogue;
                    questManager.returnToWizardQuestStatus = QuestManager.ReturnToWizardQuestStatus.CantStart;
                }
                if (questManager.CurrentQuestIndex == requiredQuestIndex && questManager.returnToWizardQuestStatus == QuestManager.ReturnToWizardQuestStatus.CantStart)
                {
                    questManager.returnToWizardQuestStatus = QuestManager.ReturnToWizardQuestStatus.NotStarted;
                }
                if (questManager.returnToWizardQuestStatus == QuestManager.ReturnToWizardQuestStatus.NotStarted && questManager.CurrentQuestIndex == requiredQuestIndex)
                {
                    sentences = notStartedDialogue;
                    questManager.returnToWizardQuestStatus = QuestManager.ReturnToWizardQuestStatus.InProgress;
                }
                else if (questManager.returnToWizardQuestStatus == QuestManager.ReturnToWizardQuestStatus.InProgress)
                {
                    sentences = inPogressDialogue;
                }
                else if (questManager.returnToWizardQuestStatus == QuestManager.ReturnToWizardQuestStatus.Completed)
                {
                    sentences = completionDialogue;
                    questManager.CurrentQuestIndex++;
                    questManager.returnToWizardQuestStatus = QuestManager.ReturnToWizardQuestStatus.After;
                }
                else if (questManager.returnToWizardQuestStatus == QuestManager.ReturnToWizardQuestStatus.After)
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
