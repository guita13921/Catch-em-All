using UnityEngine;

[CreateAssetMenu(fileName = "New Bait", menuName = "Item/Bait")]
public class Bait : Item
{
    public BaitLevel levels;

    public float GetCatchChance(int level)
    {
        if (levels != null)
            return levels.catchChance;

        Debug.LogWarning($"Bait level {level} not found on {itemName}.");
        return 0f;
    }

    private void OnEnable()
    {
        itemType = ItemType.Bait; // Ensure type is correctly set
    }
}
