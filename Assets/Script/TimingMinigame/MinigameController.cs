using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public MinigameStage minigameStage;
    [SerializeField] TimingMinigame timingMinigame;

    public enum MinigameStage
    {
        Tap_Minigame,
        Lift_Minigame,
        Swipe_Minigame,
        Joystick_Minigame,
        Timing_Minigame,
        None
    }

    void Awake()
    {
        minigameStage = MinigameStage.None;
        timingMinigame = GetComponentInChildren<TimingMinigame>();
    }

    public void HandelTap()
    {
        switch (minigameStage)
        {
            case MinigameStage.Timing_Minigame:
                timingMinigame.PressTap();
                Debug.Log("PressTap");
                break;
            case MinigameStage.None:
                // code block
                break;
            default:
                // code block
                break;
        }

    }
}
