using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 3;
    public List<ItemData> items = new List<ItemData>();
    public Sprite DefaultIcon;
    public InventoryUI inventoryUI;
    public QuestManager questManager;
    public void AddItem(ItemData item)
    {
        if (items.Count < slotCount)
        {
            items.Add(item);
            inventoryUI.UpdateUI();
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItemsByValue(ItemData itemToRemove, int amount)
    {
        int removedCount = 0;
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i] == itemToRemove && removedCount < amount)
            {
                // Debug.Log("Removing item: " + itemToRemove.itemName);
                items.RemoveAt(i);
                removedCount++;
            }
        }
        inventoryUI.UpdateUI();
    }

    public void CheckForQuestItem()
    {
        foreach (var quest in questManager.quests) 
        {
            if (quest.isStarted && !quest.isCompleted)
            {
                foreach (var item in items)
                {
                    if (item.itemName == quest.Name)
                    {
                        quest.CompleteQuest();
                        Debug.Log("Quest completed: " + quest.Name);
                        break;
                    }
                }
            }
        }
    }
}