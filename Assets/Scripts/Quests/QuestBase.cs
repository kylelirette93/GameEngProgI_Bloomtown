using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Create a new quest")]
public class QuestBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;

    [SerializeField] string[] startDialogue, inProgressDialogue, completedDialogue;

    [SerializeField] ItemData requiredItem;

    public string Name => name;
    public string Description => description;
    public string[] StartDialogue => startDialogue;
    public string[] InProgressDialogue => inProgressDialogue?.Length > 0 ? inProgressDialogue : startDialogue;

    public string[] CompletedDialogue => completedDialogue;
    public ItemData RequiredItem => requiredItem;
}
