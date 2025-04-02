using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemIcon; // Store the item icon
    public string itemName; //store the item name
    public string infoMessage; //store info message
    public int questIndex; //store quest index.
    // Add other relevant data...

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ItemData other = (ItemData)obj;
        return itemName == other.itemName;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}