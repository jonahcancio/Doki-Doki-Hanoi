using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinCatcher : MonoBehaviour {

    private int heightToWin = GameConstants.heightToWin;

    public GameObject towerBindedTo;
    private EventBus eventBus;
    private TowerStack towerStack;

    public Sprite bestGirlSprite;
    [TextArea (2, 3)]
    public string bestGirlRemark;
    public string bestGirlName;

    void Start () {
        // initialize tower components
        this.eventBus = this.towerBindedTo.GetComponent<EventBus> ();
        this.towerStack = this.towerBindedTo.GetComponent<TowerStack> ();

        // subscribe best girl checking to whenever tower gains a new block
        this.eventBus.TopBlockGainEvent += CheckIfBestGirl;
    }

    // checks if towerBindedTo's stack has reached height to win and emits win event if true
    void CheckIfBestGirl (Transform tower) {
        if (this.towerStack.GetTowerHeight () >= heightToWin) {
            // emit win event for game controller to react to
            this.EmitHeightToWinReachedEvent ();
        }
    }

    // win event that game controller will subscribe to
    public Action<WinCatcher> HeightToWinReachedEvent;
    void EmitHeightToWinReachedEvent () {
        if (this.HeightToWinReachedEvent != null) {
            this.HeightToWinReachedEvent (this);
        }
    }
}