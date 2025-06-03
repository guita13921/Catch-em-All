using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaitLevel
{
    public int level;
    [Range(0f, 1f)]
    public float catchChance; // 0 = 0%, 1 = 100%
}
