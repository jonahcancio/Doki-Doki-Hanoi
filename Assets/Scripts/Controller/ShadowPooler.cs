﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPooler : MonoBehaviour {

    public GameObject blockPrefab;

    private GameObject fromShadow;
    private GameObject toShadow;
    private Transform gameArea;

    void OnEnable () {
        //instantiate shadow blocks
        this.fromShadow = Instantiate (blockPrefab, this.transform);
        this.fromShadow.name = "Shadow";
        this.fromShadow.GetComponent<Block> ().Shadowize ();
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
            eventBus.OnEnterWhileDraggingEvent (this.ShowToShadow);
            eventBus.OnExitWhileDraggingEvent (this.HideToShadow);
            eventBus.OnLeftMouseReleaseEvent (this.HideToShadow);
            eventBus.OnLeftPressEvent (this.ShowFromShadow);
            eventBus.OnLeftMouseReleaseEvent (this.HideFromShadow);
        }
    }

    void ShowToShadow (Transform toTower, Transform fromTower) {
        if (toTower != fromTower) {
            // find toTower slot and place toShadow block inside it
            TowerStack toTowerStack = toTower.GetComponent<TowerStack> ();
            Transform toSlot = toTowerStack.GetTopSlot ();
            this.toShadow.transform.SetParent (toSlot);
            this.toShadow.SetActive (true);

            // Decide color of shadow block based on validity of fromTower's topBlock move
            Transform fromBlock = fromTower.GetComponent<TowerStack> ().GetTopBlock ();
            Color shadowColor;
            if (toTowerStack.CanSupportNewTopBlock (fromBlock)) {
                shadowColor = new Color (0f, 1f, 0f, 0.7f); // Green for valid
            } else {
                shadowColor = new Color (1f, 0f, 0f, 0.7f); // Red for invalid
            }

            // Adjust shadow's size and color
            Block toShadowData = this.toShadow.GetComponent<Block> ();
            toShadowData.ResetPosition ();
            toShadowData.SetColor (shadowColor);
            int blockNum = fromBlock.GetComponent<Block> ().blockNum;
            toShadowData.SetBlockNum (blockNum);
        }
    }

    void HideToShadow () {
        this.toShadow.transform.SetParent (this.transform);
        this.toShadow.GetComponent<Block> ().ResetPosition ();
        this.toShadow.SetActive (false);
    }

    void ShowFromShadow (Transform fromTower) {
        // find toTower slot and place toShadow block inside it
        TowerStack fromTowerStack = fromTower.GetComponent<TowerStack> ();
        Transform fromSlot = fromTowerStack.GetTopBlockSlot ();
        if (fromSlot) {
            this.fromShadow.transform.SetParent (fromSlot);
            this.fromShadow.SetActive (true);
        }

        // Adjust shadow's size and color
        Block fromShadowData = this.fromShadow.GetComponent<Block> ();
        fromShadowData.ResetPosition ();
        fromShadowData.SetColor (new Color (1f, 1f, 1f, 0.7f));
        Transform fromBlock = fromTowerStack.GetTopBlock ();
        if (fromBlock) {
            int blockNum = fromBlock.GetComponent<Block> ().blockNum;
            fromShadowData.SetBlockNum (blockNum);
        }
    }

    void HideFromShadow () {
        this.fromShadow.transform.SetParent (this.transform);
        this.fromShadow.GetComponent<Block> ().ResetPosition ();
        this.fromShadow.SetActive (false);
    }
}