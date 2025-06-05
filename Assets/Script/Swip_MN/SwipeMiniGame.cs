using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMiniGame : MonoBehaviour
{

    TensionMeterManager tensionMeterManager;

    void Awake()
    {
        tensionMeterManager = FindAnyObjectByType<TensionMeterManager>();
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        //Show UI
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleSwipe()
    {
        tensionMeterManager.AddPullForcePlayer(5f);
    }

}
