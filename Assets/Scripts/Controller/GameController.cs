using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public enum GamePhase {
        Intro,
        Game,
        Ending
        };

        public GamePhase gamePhase;
        public GameObject introBackground;
        public GameObject hostGirl;

        public GameObject endingPanel;
        public DialogueHandler dialogHandler;

        public WinCatcher[] bestGirls;
        public BlockPooler blockPooler;

        void Start () {
        if (!introBackground) {
        introBackground = GameObject.FindWithTag ("IntroBackground");
        }
        if (!hostGirl) {
            hostGirl = GameObject.FindWithTag ("HostGirl");
        }

        if (!endingPanel) {
            endingPanel = GameObject.FindWithTag ("EndingPanel");
        }

        this.gamePhase = GamePhase.Intro;
    }

    void Update () {
        if (this.gamePhase == GamePhase.Intro && this.dialogHandler.isFinishedDialoguing) {
            this.TransitionToGame ();
        }

        if (this.gamePhase == GamePhase.Game) {
            foreach (WinCatcher girl in this.bestGirls) {
                if (girl.IsSheBestGirl ()) {
                    this.TransitionToEnding (girl);
                }
            }
        }
    }

    public void TransitionToGame () {
        this.gamePhase = GamePhase.Game;
        introBackground.SetActive (false);
        hostGirl.SetActive (false);
    }

    public void TransitionToEnding (WinCatcher bestGirl) {
        this.gamePhase = GamePhase.Ending;
        this.endingPanel.SetActive (true);
        EndingHandler endingHandler = this.endingPanel.GetComponent<EndingHandler>();
        endingHandler.SetBestGirlName(bestGirl.bestGirlName);
        endingHandler.SetBestGirlRemark(bestGirl.bestGirlRemark);
        endingHandler.SetBestGirlSprite(bestGirl.bestGirlSprite);
    }

    public void TransitionResetGame () {
        this.blockPooler.ResetBlocksToInitialTower();
        this.endingPanel.SetActive(false);
        this.gamePhase = GamePhase.Game;        
    }
}