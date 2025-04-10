using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string Name;
    public string Description;
    public string itemName;
    public int requiredIndex;
    public bool canStart;
    public bool isStarted = false;
    public bool isCompleted = false;
}