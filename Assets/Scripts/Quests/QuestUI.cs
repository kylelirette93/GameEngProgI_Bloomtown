using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questDisplay;
    public Quest activeQuest;

    private void Start()
    {

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

    }

}
