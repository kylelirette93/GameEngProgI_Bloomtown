using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slotCount = 3;
    public List<ItemData> items = new List<ItemData>();
    public Sprite DefaultIcon;
    public InventoryUI inventoryUI;
    public QuestManager questManager;
    public ProgressTracker progressTracker;
    public int flowerCount = 0;

    public void AddItem(ItemData item, int amount)
    {
        if (items.Count < slotCount)
        {
            if (item.itemName == "Purple Flower")
            {
                progressTracker.flowersPicked += amount;
                questManager.CheckQuestStatus();
            }
            if (item.itemName == "Mushroom")
            {
                progressTracker.mushroomsPicked += amount;
                questManager.CheckQuestStatus();
            }
            if (item.itemName == "Apple")
            {
                progressTracker.applesPicked += amount;
                questManager.CheckQuestStatus();
            }
            {
                
            }
            items.Add(item);
            inventoryUI.UpdateUI();
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveAll()
    {
        // Remove all items from inventory when quest is completed.
        items.Clear();
        inventoryUI.UpdateUI();
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
}