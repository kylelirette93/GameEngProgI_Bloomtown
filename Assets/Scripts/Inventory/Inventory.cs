using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int slotCount = 3;
    public List<ItemData> items = new List<ItemData>();

    public Inventory()
    {

    }
    public void AddItem(ItemData item)
    {
        if (items.Count < slotCount)
        {
            items.Add(item);
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
    }

    public Inventory GetInventory()
    {
        return this;
    }
}