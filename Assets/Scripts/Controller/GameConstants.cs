using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {
    public static int maxTowerHeight = 7;

    public static int initialBlockCount = 5;
    public static int heightToWin = 5;

    public static string defaultHeader = "Instructions";
    public static string defaultInstructions =
        @"Controls:
        Left Click – drag and drop tower blocks
        Middle Click – add extra tower blocks
        Right Click – remove biggest tower blocks
    Objective:
        Stack as many tower blocks on the girl of your choice to make her happy. 
        Bigger blocks CANNOT be placed on top of smaller blocks. 
    ";
}