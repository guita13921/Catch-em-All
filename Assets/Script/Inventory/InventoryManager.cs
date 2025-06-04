using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();

    public Item baitLV01;
    public Item baitLV02;
    public Item baitLV03;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AddItem(baitLV01, 1);
        AddItem(baitLV02, 1);
        AddItem(baitLV03, 1);
    }

    public void AddItem(Item item, int quantity = 1)
    {
        InventoryItem existing = inventory.Find(i => i.item == item);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem(item, quantity));
        }
        Debug.Log($"Added {quantity}x {item.itemName} ({item.itemType})");
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        InventoryItem invItem = inventory.Find(i => i.item == item);
        if (invItem != null && invItem.quantity >= quantity)
        {
            invItem.quantity -= quantity;
            if (invItem.quantity <= 0)
                inventory.Remove(invItem);

            Debug.Log($"Removed {quantity}x {item.itemName}");
            return true;
        }
        Debug.LogWarning("Item not found or not enough quantity.");
        return false;
    }

    public List<Bait> GetBaitItems()
    {
        List<Bait> baits = new List<Bait>();
        foreach (InventoryItem inv in inventory)
        {
            if (inv.item is Bait bait)
                baits.Add(bait);
        }
        return baits;
    }

    public void PrintInventory()
    {
        Debug.Log("=== Inventory ===");
        foreach (InventoryItem i in inventory)
        {
            Debug.Log($"{i.item.itemName} ({i.item.itemType}) x{i.quantity}");
        }
    }
}
