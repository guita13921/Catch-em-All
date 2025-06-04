using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishing/Fish")]
public class Fish : ScriptableObject
{
    [Header("Basic Info")]
    public string fishName;
    public Sprite spriteModel;
    [TextArea]
    public string description;

    [Header("MInigame")]
    //public int requiredFishingLevel; สำหรับจำกัด minimum level bait
    public bool hasTap_Minigame;
    public bool hasLift_Minigame;
    public bool hasSwipe_Minigame;
    public bool hasJoystick_Minigam;
    public bool hasTiming_Minigame;

    [Header("Additional Data")]
    public FishRarity rarity;
    public TimeWindow catchTime;

    [Header("Force")]
    public float force;
    public float cooldown;

    [Header("Item Drop")]
    public Item dropItem;

    // Add other attributes like price, seasons, weather, etc.
}

public enum FishRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

[System.Serializable]
public class TimeWindow
{
    [Range(0, 24)] public int startHour;
    [Range(0, 24)] public int endHour;

    public bool IsCatchableNow(int currentHour)
    {
        return currentHour >= startHour && currentHour <= endHour;
    }
}
