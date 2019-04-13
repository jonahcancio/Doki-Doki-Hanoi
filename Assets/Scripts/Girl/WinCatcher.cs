using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCatcher : MonoBehaviour {

    private int heightToWin = GameConstants.heightToWin;


    public TowerStack towerBindedTo;
    public Sprite bestGirlSprite;
    [TextArea(2,3)]
    public string bestGirlRemark;
    public string bestGirlName;

    public bool IsSheBestGirl() {
        return this.towerBindedTo.GetTowerHeight() >= heightToWin;
    }

    void Update() {
        // if(this.towerBindedTo.GetTowerHeight() >= 3) {
        //     Debug.Log(this.towerBindedTo.GetTowerHeight());
        // }
    }
}