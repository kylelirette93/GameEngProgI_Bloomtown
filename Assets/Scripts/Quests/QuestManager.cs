using UnityEngine;

public enum QuestType { TakeFlower, TakeMushrooms, PickApples, HuntRabbits, }
public class QuestManager : MonoBehaviour
{
    Inventory inventory;
    ProgressTracker progressTracker;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        progressTracker = GameManager.Instance.progressTracker;
    }
    public enum TakeFlowerQuestStatus { NotStarted, InProgress, Completed, After }
    public TakeFlowerQuestStatus takeFlowerQuestStatus = TakeFlowerQuestStatus.NotStarted;

    public enum TakeMushroomsQuestStatus { NotStarted, InProgress, Completed, After }
    public TakeMushroomsQuestStatus takeMushroomsQuestStatus = TakeMushroomsQuestStatus.NotStarted;

    public enum PickApplesQuestStatus { NotStarted, InProgress, Completed, After }
    public PickApplesQuestStatus pickApplesQuestStatus = PickApplesQuestStatus.NotStarted;

    public enum HuntRabbitsQuestStatus { NotStarted, InProgress, Completed, After }
    public HuntRabbitsQuestStatus huntRabbitsQuestStatus = HuntRabbitsQuestStatus.NotStarted;

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
    }
}
