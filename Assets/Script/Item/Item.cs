using UnityEngine;

public enum ItemType
{
    Bait,
    FishItem,
    FoodItem,
    ingredientItem
}

public abstract class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite icon;
    public int price;

    [TextArea]
    public string description;
}
