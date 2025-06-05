using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static Enums;

public class MinigameController : MonoBehaviour
{
    GameMainManager gameMainManager;
    TensionMeterManager tensionMeterManager;

    [SerializeField] TimingMinigame timingMinigame;
    [SerializeField] TapMiniGame tapMiniGame;
    [SerializeField] LiftMiniGame liftMiniGame;
    [SerializeField] SwipeMiniGame swipeMiniGame;
    [SerializeField] JoystickMiniGame joystickMiniGame;


    void Awake()
    {
        gameMainManager = GetComponentInParent<GameMainManager>();
        tensionMeterManager = GetComponent<TensionMeterManager>();
    }

    public void StartMiniGame(Fish currentFish)
    {
        gameMainManager.isPlayingMiniGame = true;

        List<string> availableMinigames = new List<string>();

        // Read available minigames from the fish
        if (currentFish.hasTap_Minigame) availableMinigames.Add("Tap");
        if (currentFish.hasLift_Minigame) availableMinigames.Add("Lift");
        if (currentFish.hasSwipe_Minigame) availableMinigames.Add("Swipe");
        if (currentFish.hasJoystick_Minigam) availableMinigames.Add("Joystick");
        if (currentFish.hasTiming_Minigame) availableMinigames.Add("Timing");

        if (availableMinigames.Count == 0)
        {
            UnityEngine.Debug.LogWarning("No minigames available for this fish.");
            gameMainManager.isPlayingMiniGame = false;
            return;
        }

        string selectedMinigame = availableMinigames[Random.Range(0, availableMinigames.Count)];
        UnityEngine.Debug.Log($"🎮 Starting {selectedMinigame} Minigame!");

        switch (selectedMinigame)
        {
            case "Tap":
                gameMainManager.gameStage = GameStage.Tap_Minigame;
                tapMiniGame = GetComponentInChildren<TapMiniGame>();
                tapMiniGame.StartGame();
                break;

            case "Timing":
                gameMainManager.gameStage = GameStage.Timing_Minigame;
                timingMinigame = GetComponentInChildren<TimingMinigame>();
                timingMinigame.StartGame();
                break;

            // Extend here as you implement more
            case "Lift":
                gameMainManager.gameStage = GameStage.Lift_Minigame;
                liftMiniGame = GetComponentInChildren<LiftMiniGame>();
                liftMiniGame.StartGame();
                break;

            case "Swipe":
                gameMainManager.gameStage = GameStage.Swipe_Minigame;
                swipeMiniGame = GetComponentInChildren<SwipeMiniGame>();
                swipeMiniGame.StartGame();
                break;

            case "Joystick":
                gameMainManager.gameStage = GameStage.Joystick_Minigame;
                joystickMiniGame = GetComponentInChildren<JoystickMiniGame>();
                joystickMiniGame.StartGame();
                break;
        }
    }

    public void ChangeMinigame(Fish currentFish)
    {
        //Play VFX for change minigame 
        //delay ...
        gameMainManager.gameStage = GameStage.ChangingMiniGame;
        StartMiniGame(currentFish);

    }

    public void Tap_MinigamePressTab() //Show UI control UI 
    {
        tapMiniGame.PressTab();
    }

    public void Timing_MinigamePressTab()
    {
        timingMinigame.PressTab();
    }

    public void lift_MinigameLift()
    {
        liftMiniGame.HandleLift();
    }

    public void Swipe_MinigameSwipe()
    {
        swipeMiniGame.HandleSwipe();
    }

    public void Joystick_MinigameRoll()
    {
        joystickMiniGame.MNHandleRoll();
    }
}