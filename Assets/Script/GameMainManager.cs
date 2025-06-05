using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GameMainManager : MonoBehaviour
{
    public MinigameController minigameController;
    public FishingController fishingController;
    public GameStage gameStage;
    public bool isPlayingMiniGame;

    void Awake()
    {
        gameStage = GameStage.Mainmenu;
        minigameController = GetComponentInChildren<MinigameController>();
        fishingController = GetComponent<FishingController>();
    }

    public void HandelTap()
    {
        switch (gameStage)
        {
            case GameStage.Timing_Minigame:
                minigameController.Timing_MinigamePressTab();
                break;

            case GameStage.Tap_Minigame:
                minigameController.Tap_MinigamePressTab();
                break;

            case GameStage.None:
                break;

            default:
                break;
        }
    }

    public void HandelHold()
    {
        switch (gameStage)
        {
            case GameStage.PrepareFishing:
                fishingController.StartThrowBait();
                break;

            case GameStage.None:
                break;

            default:

                break;
        }
    }

    public void HandelLift()
    {
        switch (gameStage)
        {
            case GameStage.BaitedFish:
                fishingController.PlayerLiftedRod();
                break;

            case GameStage.Lift_Minigame:
                minigameController.lift_MinigameLift();
                break;

            default:

                break;
        }
    }

    public void HandelSwipe()
    {
        switch (gameStage)
        {
            case GameStage.Swipe_Minigame:
                minigameController.Swipe_MinigameSwipe();
                break;

            default:

                break;
        }
    }

    public void HandleRoll()
    {
        switch (gameStage)
        {
            case GameStage.Joystick_Minigame:
                minigameController.Joystick_MinigameRoll();
                break;

            default:

                break;
        }

    }
}
