using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GameMainManager : MonoBehaviour
{
    InputController inputController;
    MinigameController minigameController;
    FishingController fishingController;

    public GameStage gameStage;

    void Awake()
    {
        gameStage = GameStage.Mainmenu;
        inputController = GetComponent<InputController>();
        minigameController = GetComponent<MinigameController>();
        fishingController = GetComponent<FishingController>();
    }

    public void HandelTap()
    {
        switch (gameStage)
        {
            case GameStage.Timing_Minigame:
                minigameController.Timing_MinigamePressTap();
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

            case GameStage.None:
                break;

            default:

                break;
        }
    }


}
