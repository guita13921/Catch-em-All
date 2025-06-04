using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FishItem", menuName = "Item/FishItem")]
public class BaiFishItemt : Item
{
    public BaitLevel levels;
    public float weight;
    public FishRarity fishRarity;

}
