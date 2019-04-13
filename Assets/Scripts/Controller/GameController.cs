using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public enum GameMode {
        Intro,
        Game,
        Ending
    };

    public GameMode gameMode;
    private GameObject introBackground;
    private GameObject hostGirl;

    void Start () {
        introBackground = GameObject.FindWithTag ("IntroBackground");
        hostGirl = GameObject.FindWithTag("HostGirl");
    }

    public void TransitionToIntro() {

    }

    public void TransitionToGame() {
        this.gameMode = GameMode.Game;
        introBackground.SetActive(false);
        hostGirl.SetActive(false);
    }

    public void TransitionToEnding() {

    }
}