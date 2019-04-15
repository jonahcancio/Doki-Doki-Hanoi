using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {
    // maximum number of blocks the game can support
    public static int maxTowerHeight = 8;

    // initial number of blocks to start initialTower with
    public static int initialBlockCount = 5;
    // initial height needed to win
    public static int defaultHeightToWin = 5;

    // default dialogue for instructions in game
    public static string defaultHeader = "Instructions";
    public static string defaultInstructions =
        @"Controls:
        Left Click – drag and drop keys to other girls
        Middle Click – add extra keys to Monika
        Right Click – reduce the number of keys in game area by removing the BIGGEST key
Objective:
        Stack as many keys on the girl of your choice to make her happy. 
        Bigger keys CANNOT be placed on top of smaller keys. 
    ";
}