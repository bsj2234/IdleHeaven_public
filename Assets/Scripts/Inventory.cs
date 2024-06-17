using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void AddItem(Item item);
    void RemoveItem(Item item);
    bool ContainsItem(Item item);
    Item FindItemByName(string itemName);
    IEnumerable<Item> GetItems();
}

public class Inventory : MonoBehaviour, IInventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
    public Item FindItemByName(string itemName)
    {
        return items.Find(other => itemName == other.Name);
    }

    public IEnumerable<Item> GetItems()
    {
        return items;
    }

    public List<Item> GetAllItems()
    {
        return items;
    }
}

// Item.cs
[System.Serializable]
public class Item
{
    public string Name;
    public Sprite Icon;
    // Additional item properties
}