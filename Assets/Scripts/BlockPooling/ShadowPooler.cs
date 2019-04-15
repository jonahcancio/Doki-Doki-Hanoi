using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPooler : MonoBehaviour {

    public GameObject blockPrefab;

    // shadow used to highlight where block was from
    private GameObject fromShadow;
    // shadow used to highlight where block will drop
    private GameObject toShadow;
    private Transform gameArea;

    void OnEnable () {
        //instantiate shadow blocks
        this.fromShadow = Instantiate (blockPrefab, this.transform);
        this.fromShadow.name = "Shadow";
        // this.fromShadow.GetComponent<Block> ().Shadowize ();
        this.fromShadow.SetActive (false);

        this.toShadow = Instantiate (blockPrefab, this.transform);
        this.toShadow.name = "Shadow";
        // this.toShadow.GetComponent<Block>().Shadowize();
        this.toShadow.SetActive (false);
    }

    void Start () {
        this.gameArea = GameObject.FindWithTag ("GameArea").transform;
        // subscribe to shadow events
        foreach (Transform tower in this.gameArea) {
            EventBus eventBus = tower.GetComponent<EventBus> ();
            eventBus.EnterWhileDraggingEvent  += this.ShowToShadow;
            eventBus.ExitWhileDraggingEvent += this.HideToShadow;
            eventBus.LeftReleaseEvent += this.HideToShadow;
            eventBus.LeftPressEvent += this.ShowFromShadow;
            eventBus.LeftReleaseEvent += this.HideFromShadow;
        }
    }

    // show shadow of where block will drop on toTower
    void ShowToShadow (Transform toTower) {
        Transform fromTower = MouseActionTrigger.towerBeingDragged;
        if (toTower != fromTower) {
            Transform fromBlock = fromTower.GetComponent<TowerStack> ().GetTopBlock ();
            if (fromBlock) {
                // find toTower slot and place toShadow block inside it
                TowerStack toTowerStack = toTower.GetComponent<TowerStack> ();
                Transform toSlot = toTowerStack.GetTopSlot ();
                this.toShadow.transform.SetParent (toSlot);
                this.toShadow.SetActive (true);

                // Decide color of shadow block based on validity of fromTower's topBlock move
                Color shadowColor;
                if (toTowerStack.CanSupportNewTopBlock (fromBlock)) {
                    shadowColor = new Color (0f, 1f, 0f, 0.6f); // Green for valid
                } else {
                    shadowColor = new Color (1f, 0f, 0f, 0.6f); // Red for invalid
                }

                // Adjust shadow's size and color
                Block toShadowData = this.toShadow.GetComponent<Block> ();
                toShadowData.ResetPosition ();
                toShadowData.SetColor (shadowColor);
                int blockNum = fromBlock.GetComponent<Block> ().blockNum;
                toShadowData.SetBlockNum (blockNum);
            }
        }
    }

    // hide shadow by setting it to inactive
    void HideToShadow (Transform toTower) {
        this.toShadow.transform.SetParent (this.transform);
        this.toShadow.GetComponent<Block> ().ResetPosition ();
        this.toShadow.SetActive (false);
    }

    // show shadow of where block came from on fromTower
    void ShowFromShadow (Transform fromTower) {
        // find toTower slot and place toShadow block inside it
        TowerStack fromTowerStack = fromTower.GetComponent<TowerStack> ();
        Transform fromBlock = fromTowerStack.GetTopBlock ();
        if (fromBlock) {
            Transform fromSlot = fromTowerStack.GetTopBlockSlot ();
            if (fromSlot) {
                this.fromShadow.transform.SetParent (fromSlot);
                this.fromShadow.SetActive (true);
            }

            // Adjust shadow's size and color
            Block fromShadowData = this.fromShadow.GetComponent<Block> ();
            fromShadowData.ResetPosition ();
            fromShadowData.SetColor (new Color (0f, 0f, 0f, 0.6f)); //black shadow
            int blockNum = fromBlock.GetComponent<Block> ().blockNum;
            fromShadowData.SetBlockNum (blockNum);
        }

    }

    // hide shadow by setting it to inactive
    void HideFromShadow (Transform fromShadow) {
        this.fromShadow.transform.SetParent (this.transform);
        this.fromShadow.GetComponent<Block> ().ResetPosition ();
        this.fromShadow.SetActive (false);
    }
}