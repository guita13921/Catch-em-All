using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class MinigameController : MonoBehaviour
{
    GameMainManager gameMainManager;

    // Minigame
    TimingMinigame timingMinigame;

    void Awake()
    {
        timingMinigame = GetComponentInChildren<TimingMinigame>();
    }

    public void Tap_Minigame()
    {

    }

    public void Lift_Minigame()
    {

    }

    public void FirstLift_Minigame()
    {

    }

    public void Swipe_Minigame()
    {

    }

    public void Joystick_Minigame()
    {

    }

    public void Timing_MinigamePressTap()
    {
        timingMinigame.PressTap();
    }
}