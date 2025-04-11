using UnityEngine;

public enum QuestType { TakeFlower, TakeMushrooms, PickApples, HuntRabbits, ReturnToWizard }
public class QuestManager : MonoBehaviour
{
    Inventory inventory;
    ProgressTracker progressTracker;
    public int CurrentQuestIndex = 0;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        progressTracker = GameManager.Instance.progressTracker;
    }
    public enum TakeMushroomsQuestStatus { NotStarted, InProgress, Completed, After }
    public TakeMushroomsQuestStatus takeMushroomsQuestStatus = TakeMushroomsQuestStatus.NotStarted;

    public enum TakeFlowerQuestStatus { CantStart, NotStarted, InProgress, Completed, After }
    public TakeFlowerQuestStatus takeFlowerQuestStatus = TakeFlowerQuestStatus.NotStarted;

    public enum PickApplesQuestStatus { CantStart, NotStarted, InProgress, Completed, After }
    public PickApplesQuestStatus pickApplesQuestStatus = PickApplesQuestStatus.NotStarted;

    public enum HuntRabbitsQuestStatus { CantStart, NotStarted, InProgress, Completed, After }
    public HuntRabbitsQuestStatus huntRabbitsQuestStatus = HuntRabbitsQuestStatus.NotStarted;

    public enum ReturnToWizardQuestStatus { CantStart, NotStarted, InProgress, Completed, After }
    public ReturnToWizardQuestStatus returnToWizardQuestStatus = ReturnToWizardQuestStatus.NotStarted;

    public void CheckQuestStatus()
    {
        if (progressTracker.flowersPicked >= 3)
        {
            takeFlowerQuestStatus = TakeFlowerQuestStatus.Completed;
        }
        if (progressTracker.mushroomsPicked >= 3)         
        {
            takeMushroomsQuestStatus = TakeMushroomsQuestStatus.Completed;
        }
        if (progressTracker.applesPicked >= 3)
        {
            pickApplesQuestStatus = PickApplesQuestStatus.Completed;
        }
        if (progressTracker.rabbitsHunted >= 3)
        {
            huntRabbitsQuestStatus = HuntRabbitsQuestStatus.Completed;
        }
        if (progressTracker.wizardReturnedTo)
        {
            returnToWizardQuestStatus = ReturnToWizardQuestStatus.Completed;
        }
    }
}
