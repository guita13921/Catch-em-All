using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMiniGame : MonoBehaviour
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

    public void PressTab()
    {
        tensionMeterManager.AddPullForcePlayer(1f);
    }
}
