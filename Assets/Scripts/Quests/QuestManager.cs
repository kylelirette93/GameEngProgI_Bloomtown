using UnityEngine;

public enum QuestType { PickFlower, PickMushrooms, TalkToElders, HuntRabbits, }
public class QuestManager : MonoBehaviour
{
    Inventory inventory;
    ProgressTracker progressTracker;

    void Start()
    {
        inventory = GameManager.Instance.UIManager.inventory;
        progressTracker = GameManager.Instance.progressTracker;
    }
    public enum PickFlowerQuestStatus { NotStarted, InProgress, Completed, After }
    public PickFlowerQuestStatus pickFlowerQuestStatus = PickFlowerQuestStatus.NotStarted;

    public enum PickMushroomsQuestStatus { NotStarted, InProgress, Completed, After }
    public PickMushroomsQuestStatus pickMushroomsQuestStatus = PickMushroomsQuestStatus.NotStarted;

    public enum TalkToEldersQuestStatus { NotStarted, InProgress, Completed, After }
    public TalkToEldersQuestStatus talkToEldersQuestStatus = TalkToEldersQuestStatus.NotStarted;

    public enum HuntRabbitsQuestStatus { NotStarted, InProgress, Completed, After }
    public HuntRabbitsQuestStatus huntRabbitsQuestStatus = HuntRabbitsQuestStatus.NotStarted;

    public void CheckQuestStatus()
    {
        if (progressTracker.flowersPicked >= 3)
        {
            pickFlowerQuestStatus = PickFlowerQuestStatus.Completed;
        }
        if (progressTracker.mushroomsPicked >= 3)         
        {
            pickMushroomsQuestStatus = PickMushroomsQuestStatus.Completed;
        }
        if (progressTracker.eldersTalkedTo >= 3)
        {
            talkToEldersQuestStatus = TalkToEldersQuestStatus.Completed;
        }
        if (progressTracker.rabbitsHunted >= 3)
        {
            huntRabbitsQuestStatus = HuntRabbitsQuestStatus.Completed;
        }
    }
}
