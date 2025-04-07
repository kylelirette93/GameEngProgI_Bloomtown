using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questDisplay;
    QuestManager questManager;
    public Quest activeQuest;

    private void Start()
    {
        questManager = GameManager.Instance.questManager;
        // Provided only one quest can be active at a time.
        //Debug.Log(questManager.questList.Count);
        activeQuest = questManager.questList[0];
    }

    private void Update()
    {
        if (questDisplay != null)
        {
            DisplayQuest();
        }
    }
    public void DisplayQuest()
    {
        questDisplay.text = "Title: " + activeQuest.title + "\n" + "Description: " + "\n" + activeQuest.description;
    }

}
