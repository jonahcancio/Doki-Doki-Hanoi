using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockPooler : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    private int initialBlockCount = GameConstants.initialBlockCount;

    public GameObject blockPrefab;
    public GameObject slotPrefab;

    public Transform gameArea;

    private Stack<GameObject> pooledBlocks;

    public Transform initialTower;

    void Start () {

        // instantiate block slots
        foreach (Transform tower in this.gameArea) {
            GameObject slot;
            for (int i = 0; i < maxTowerHeight; i++) {
                slot = Instantiate (slotPrefab, tower);
                slot.name = "Slot" + (i + 1);
            }
            tower.GetComponent<TowerStack> ().RefreshSlotIndex ();
        }

        // instantiate blocks
        this.pooledBlocks = new Stack<GameObject> ();
        GameObject block;
        for (int i = maxTowerHeight; i > 0; i--) {
            block = Instantiate (blockPrefab, this.transform);
            block.name = "Block";
            block.GetComponent<Block> ().blockNum = i;
            block.SetActive (false);
            this.pooledBlocks.Push (block);
        }

        // setup initial tower
        if (!this.initialTower) {
            initialTower = GameObject.FindWithTag ("InitialTower").transform;
        }

        // subscribe chop and grow tower events to middle and right mouse clicks
        foreach (Transform tower in this.gameArea) {
            EventBus eventBus = tower.GetComponent<EventBus> ();
            eventBus.MiddleClickEvent += this.GrowTower;
            eventBus.RightClickEvent += this.ChopTower;
        }

        // add blocks to initial tower
        ResetBlocksToInitialTower ();

    }

    // returns the number of blocks currently in game area
    public int GetBlocksInPlay () {
        return this.maxTowerHeight - this.pooledBlocks.Count;
    }

    // run through each tower and return the one with the biggest tower block
    Transform GetTowerToChop () {
        Transform maxTower = this.gameArea.GetChild (0);
        foreach (Transform tower in this.gameArea) {
            int maxBlockNum = maxTower.GetComponent<TowerStack> ().GetBottomBlockNum ();
            int blockNum = tower.GetComponent<TowerStack> ().GetBottomBlockNum ();
            if (blockNum > maxBlockNum) {
                maxTower = tower;
            }
        }
        return maxTower;
    }

    // chop the tower with the bigges tower block
    public void ChopTower (Transform towerClicked) {
        if (this.pooledBlocks.Count >= maxTowerHeight) {
            Debug.Log ("Block pool is full! What are you trying to chop");
        }
        // locate the tower that must be chopped
        Transform towerToChop = GetTowerToChop ();

        // acquire the bottom bottom block from tower
        Transform block = towerToChop.GetComponent<TowerStack> ().ChopTowerFromBelow ();
        if (block != null) {
            // push block back to pool stack
            block.SetParent (this.transform);
            block.gameObject.SetActive (false);
            this.pooledBlocks.Push (block.gameObject);
            this.EmitTowerChopEvent(towerToChop);
        }
    }

    public Action<Transform> TowerChopEvent;
    void EmitTowerChopEvent(Transform towerChopped) {
        if(this.TowerChopEvent != null) {
            this.TowerChopEvent(towerChopped);
        }
    }

    // grow the initial tower
    public void GrowTower (Transform towerClicked) {
        if (this.pooledBlocks.Count <= 0) {
            Debug.Log ("No more pooled blocks left");
        } else {
            // pop block from stack
            GameObject block = this.pooledBlocks.Pop ();
            block.SetActive (true);

            // grow tower stack
            initialTower.GetComponent<TowerStack> ().GrowTowerFromBelow (block.transform);
        }
        this.EmitTowerGrowEvent(initialTower);
    }

    public Action<Transform> TowerGrowEvent;
    void EmitTowerGrowEvent(Transform towerGrown) {
        if(this.TowerGrowEvent != null) {
            this.TowerGrowEvent(towerGrown);
        }
    }


    // reset all blocks of the tower back to initial game state
    // called by controller upon game reset
    public void ResetBlocksToInitialTower () {
        // return all blocks back to pool
        while (this.pooledBlocks.Count < maxTowerHeight) {
            this.ChopTower (null);
        }
        // add initial blocks to intial tower's stack
        for (int i = 0; i < initialBlockCount; i++) {
            this.GrowTower (null);
        }
    }
}