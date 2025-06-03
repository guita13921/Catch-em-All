using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Bait", menuName = "Fishing/Bait")]
public class Bait : ScriptableObject
{
    public string baitName;
    public Sprite spriteModel;
    [TextArea]
    public string description;

    public BaitLevel levels;

    public float GetCatchChance(int level)
    {
        BaitLevel baitLevel = levels;
        if (baitLevel != null)
            return baitLevel.catchChance;

        Debug.LogWarning($"Bait level {level} not found on {baitName}.");
        return 0f;
    }
}
