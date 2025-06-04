using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public static IconController Instance { get; private set; }

    GameMainManager gameMainManager;

    void Awake()
    {
        gameMainManager = GetComponent<GameMainManager>();
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // === Shared method for sprite clicks ===
    public void HandleSpriteButtonPress(SpriteButton.ButtonType buttonType)
    {
        switch (buttonType)
        {
            case SpriteButton.ButtonType.Start:
                OnStartPressed(); break;
            case SpriteButton.ButtonType.Settings:
                OnSettingsPressed(); break;
            case SpriteButton.ButtonType.Inventory:
                OnInventoryPressed(); break;
            case SpriteButton.ButtonType.Kitchen:
                OnKitchenPressed(); break;
            case SpriteButton.ButtonType.Shop:
                OnShopPressed(); break;
            case SpriteButton.ButtonType.ChangeBait:
                OnChangeBaitPressed(); break;
        }
    }

    // === Button Press Events ===
    public void OnStartPressed()
    {
        Debug.Log("Start button pressed");
        gameMainManager.gameStage = GameStage.PrepareFishing;
    }

    public void OnSettingsPressed()
    {
        Debug.Log("Settings button pressed");
        // Open settings UI
    }

    public void OnInventoryPressed()
    {
        Debug.Log("Inventory button pressed");
        // Open inventory screen
    }

    public void OnKitchenPressed()
    {
        Debug.Log("Kitchen button pressed");
        // Open kitchen/cooking interface
    }

    public void OnShopPressed()
    {
        Debug.Log("Shop button pressed");
        // Open shop interface
    }

    public void OnChangeBaitPressed()
    {
        Debug.Log("Change Bait pressed");
        // Open shop interface
    }
}
