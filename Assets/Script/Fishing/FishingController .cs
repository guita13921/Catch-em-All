using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Enums;

public class FishingController : MonoBehaviour
{
    GameMainManager gameMainManager;

    [Header("All Fish Data")]
    public List<Fish> allFish;

    [Header("Fishing Context")]
    public Bait currentBait;
    public Fish currentFish;
    public int currentHour;

    private bool isWaitingForFish = false;
    private bool isWaitingForLift = false;
    private bool playerLifted = false;

    void Awake()
    {
        gameMainManager = GetComponent<GameMainManager>();
    }

    void Update()
    {
        if (gameMainManager.gameStage == GameStage.WaitingFish && !isWaitingForFish)
        {
            StartCoroutine(WaitAndCatchFish()); //Call GetRandomCatchableFish
        }
        else if (gameMainManager.gameStage == GameStage.BaitedFish && !isWaitingForLift && currentFish != null)
        {
            StartCoroutine(WaitForLift());
        }
    }

    IEnumerator WaitAndCatchFish()
    {
        isWaitingForFish = true;
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        // Double check stage to ensure state didn't change during wait
        if (gameMainManager.gameStage == GameStage.WaitingFish)
        {
            currentFish = GetRandomCatchableFish();
        }

        isWaitingForFish = false;
    }

    IEnumerator WaitForLift()
    {
        isWaitingForLift = true;
        playerLifted = false;

        yield return new WaitForSeconds(2f);

        if (!playerLifted && gameMainManager.gameStage == GameStage.BaitedFish)
        {
            Debug.Log("Fish got away!");
            gameMainManager.gameStage = GameStage.PrepareFishing; //ตกพลาดกลับไปเลือกเบ็ดใหม่
            currentFish = null;
        }

        isWaitingForLift = false;
    }

    public void PlayerLiftedRod()
    {
        if (gameMainManager.gameStage == GameStage.BaitedFish && isWaitingForLift)
        {
            playerLifted = true;
            Debug.Log("Player lifted in time!");
            // Proceed to next minigame or stage
            gameMainManager.gameStage = GameStage.FirstLift_Minigame;
        }
    }

    public void StartThrowBait()
    {
        gameMainManager.gameStage = GameStage.WaitingFish;
    }

    public Fish GetRandomCatchableFish()
    {
        List<Fish> catchableFish = allFish.Where(f =>
            f.catchTime.IsCatchableNow(currentHour)
        ).ToList();

        if (catchableFish.Count == 0)
        {
            Debug.LogWarning("No catchable fish found for the current conditions.");
            return null;
        }

        float roll = Random.Range(0f, 1f);

        if (roll > currentBait.levels.catchChance)
        {
            Debug.Log($"Fish got away! (failed bait chance){currentBait.levels.catchChance} : {roll}");
            gameMainManager.gameStage = GameStage.PrepareFishing; //ตกพลาดกลับไปเลือกเบ็ดใหม่
            currentFish = null;
            return null;
        }
        else
        {
            Debug.Log($"Fish got Baited!");
            gameMainManager.gameStage = GameStage.BaitedFish; //ตกได้ทำเล่น FirstLift_Minigame
        }

        return catchableFish[Random.Range(0, catchableFish.Count)];
    }

}
