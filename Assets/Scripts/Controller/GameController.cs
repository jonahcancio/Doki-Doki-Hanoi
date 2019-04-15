using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public EventBus[] eventBuses;
        public Text blocksToWin;

        // fields used by the ending phase
        public GameObject endingPanel;

        void OnEnable () {
        if (this.gamePhase == GamePhase.Intro) {
        // start game phase at intro
        this.TransitionToIntro ();
        } else if (this.gamePhase == GamePhase.Game) {
        // start game phase at game
        this.TransitionToGame ();
        } else {
        // start game pgase at ending
        this.TransitionToEnding (null);
        }
    }

    void Start () {
        // subscribe game phase to dialogue handler's finished event
        this.dialogHandler.DialogueFinishedEvent += this.TransitionToGame;

        // subscribe ending phase to each of the best girls' height to win reached events
        foreach (GameObject girl in this.bestGirls) {
            WinCatcher girlWC = girl.GetComponent<WinCatcher> ();
            girlWC.HeightToWinReachedEvent += this.TransitionToEnding;
        }

        BlockPooler blockPooler = this.blockPooler.GetComponent<BlockPooler> ();
        blockPooler.TowerGrowEvent += this.RefreshWinObjective;
        blockPooler.TowerChopEvent += this.RefreshWinObjective;

    }

    // set up scene for intro
    public void TransitionToIntro () {
        this.blockPooler.SetActive (false);
        this.gameArea.SetActive (false);
        this.endingPanel.SetActive (false);
        this.introBackground.SetActive (true);
        this.hostGirl.SetActive (true);
    }

    // used to transition from intro to game
    public void TransitionToGame () {
        this.gamePhase = GamePhase.Game;
        this.introBackground.SetActive (false);
        this.hostGirl.SetActive (false);
        this.blockPooler.SetActive (true);
        this.gameArea.SetActive (true);
    }

    // used to transition from game to ending
    // takes a WinCatcher parameter from the best girl who won
    public void TransitionToEnding (WinCatcher bestGirl) {
        this.gamePhase = GamePhase.Ending;
        this.endingPanel.SetActive (true);
        this.gameArea.SetActive (false);
        // set best girl parameters based on who won
        if (bestGirl != null) {
            EndingHandler endingHandler = this.endingPanel.GetComponent<EndingHandler> ();
            endingHandler.SetBestGirlName (bestGirl.bestGirlName);
            endingHandler.SetBestGirlRemark (bestGirl.bestGirlRemark);
            endingHandler.SetBestGirlSprite (bestGirl.bestGirlSprite);
        }
    }

    // used to transition from ending back to game
    public void TransitionResetGame () {
        this.endingPanel.SetActive (false);
        this.gameArea.SetActive (true);
        this.blockPooler.SetActive (true);
        this.blockPooler.GetComponent<BlockPooler> ().ResetBlocksToInitialTower ();

        foreach (GameObject girl in bestGirls) {
            Emotions girlEmotions = girl.GetComponent<Emotions> ();
            girlEmotions.RefreshDefaultEmote ();
        }
        this.gamePhase = GamePhase.Game;
    }

    // used to refresh number of keys needed to win the game
    // called whenever keys are added or removed from the block pooler
    public void RefreshWinObjective (Transform towerClicked) {
        int blocksInPlay = this.blockPooler.GetComponent<BlockPooler> ().GetBlocksInPlay ();
        int winObjective = Mathf.Clamp (blocksInPlay, 5, 8);
        foreach (GameObject girl in bestGirls) {
            WinCatcher girlWC = girl.GetComponent<WinCatcher> ();
            girlWC.heightToWin = winObjective;
            girlWC.CheckIfBestGirl (null);
        }
        blocksToWin.text = "Keys Needed To Win: " + winObjective;
    }
}