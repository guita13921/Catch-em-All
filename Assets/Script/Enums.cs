using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum GameStage
    {
        Tap_Minigame,
        Lift_Minigame,
        Swipe_Minigame,
        Joystick_Minigame,
        Timing_Minigame,
        PrepareFishing, //หน้า Hold ก่อนตกปลา 
        WaitingFish, //รอปลามากินเหยื่อ
        BaitedFish, //ปลากินเหยื่อแล้ว
        FirstLift_Minigame,
        Mainmenu,
        None
    }
}
