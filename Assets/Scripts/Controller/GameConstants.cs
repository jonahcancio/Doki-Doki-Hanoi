using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {
    public static int maxTowerHeight = 8;

    public static int initialBlockCount = 5;
    public static int defaultHeightToWin = 5;

    public static string defaultHeader = "Instructions";
    public static string defaultInstructions =
        @"Controls:
        Left Click – drag and drop keys to other girls
        Middle Click – add extra keys to Monika
        Right Click – reduce the number of keys in game area
Objective:
        Stack as many keys on the girl of your choice to make her happy. 
        Bigger keys CANNOT be placed on top of smaller keys. 
    ";
}