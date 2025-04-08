using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Image[] inventorySlots;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI, items count: " + inventory.items.Count);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].sprite = inventory.items[i].itemIcon;
            }
            else
            {
                inventorySlots[i].sprite = inventory.DefaultIcon;
            }
        }
    }
}