﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // enum used to track current gamephase
    private enum GamePhase {
        Intro,
        Game,
        Ending
    };
    [SerializeField]
    private GamePhase gamePhase;

    // fields used by the intro phase
    public GameObject introBackground;
    public GameObject hostGirl;    
    public DialogueHandler dialogHandler;

    // fields used by the game phase
    public GameObject blockPooler;
    public GameObject[] bestGirls;
    public GameObject gameArea;
    
    // fields used by the ending phase
    public GameObject endingPanel;

    void Start () {
        // subscribe game phase to dialogue handler's finished event
        this.dialogHandler.DialogueFinishedEvent += this.TransitionToGame;

        // subscribe ending phase to each of the best girls' height to win reached events
        foreach (GameObject girl in bestGirls) {
            WinCatcher girlWC = girl.GetComponent<WinCatcher> ();
            girlWC.HeightToWinReachedEvent += this.TransitionToEnding;
        }

        // start game phase at intro
        this.gamePhase = GamePhase.Intro;
    }


    // used to transition from intro to game
    public void TransitionToGame () {
        this.gamePhase = GamePhase.Game;
        introBackground.SetActive (false);
        hostGirl.SetActive (false);
        this.gameArea.SetActive(true);
        this.blockPooler.SetActive(true);
    }

    // used to transition from game to ending
    public void TransitionToEnding (WinCatcher bestGirl) {
        this.gamePhase = GamePhase.Ending;
        this.endingPanel.SetActive (true);
        this.gameArea.SetActive(false);
        EndingHandler endingHandler = this.endingPanel.GetComponent<EndingHandler> ();
        endingHandler.SetBestGirlName (bestGirl.bestGirlName);
        endingHandler.SetBestGirlRemark (bestGirl.bestGirlRemark);
        endingHandler.SetBestGirlSprite (bestGirl.bestGirlSprite);
    }

    // used to transition from ending back to game
    public void TransitionResetGame () {
        this.endingPanel.SetActive (false);
        this.gameArea.SetActive(true);
        this.blockPooler.SetActive(true);
        this.blockPooler.GetComponent<BlockPooler>().ResetBlocksToInitialTower ();        
        this.gamePhase = GamePhase.Game;
    }
}