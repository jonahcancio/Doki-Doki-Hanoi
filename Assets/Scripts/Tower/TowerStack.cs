using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStack : MonoBehaviour {

    // FIELDS    
    private int maxTowerHeight = GameConstants.maxTowerHeight;
    public int slotIndex;

    private EventBus eventBus;

    void Start () {
        // initialize slot index
        this.RefreshSlotIndex();
        // subscribe block transfers to the drop event
        this.eventBus = this.GetComponent<EventBus> ();
        this.eventBus.OnTowerDropEvent (this.AttemptBlockTransferFrom);
    }

    // refreshing slot index by traversing slots from the bottom until empty slot is found
    public void RefreshSlotIndex () {
        this.slotIndex = -1;
        for (int i = this.transform.childCount - 1; i >= 0; i--) {
            Transform slot = this.transform.GetChild (i);
            if (slot.Find ("Block") == null) {
                this.slotIndex = i;
                break;
            }
        }
    }

    // COMPUTED PROPERTIES  -- functions that derive values based on fields
    // returns current height of tower
    public int GetTowerHeight () {
        return this.maxTowerHeight - this.slotIndex - 1;
    }

    // returns transform of the top block of tower
    public Transform GetTopBlock () {
        int blockIndex = this.slotIndex + 1;
        if (blockIndex < maxTowerHeight) {
            return this.transform.GetChild (blockIndex).Find ("Block");
        }
        return null;
    }

    public Transform GetTopBlockSlot () {
        if (this.slotIndex < maxTowerHeight - 1) {
            return this.transform.GetChild (this.slotIndex + 1);
        }
        return null;
    }

    public Transform GetTopSlot () {
        return this.transform.GetChild (this.slotIndex);
    }

    // returns the block number of the bottom block of tower
    public int GetBottomBlockNum () {
        Transform bottomSlot = this.transform.GetChild (maxTowerHeight - 1);
        if (bottomSlot.childCount > 0) {
            Transform bottomBlock = bottomSlot.Find ("Block");
            if (bottomBlock) {
                return bottomBlock.GetComponent<Block> ().blockNum;
            }
        }
        return 0;
    }

    // returns true if newTopBlock parameter has a lower block number than the current top block's
    // returns false otherwise; this is the main logic of Tower of Hanoi
    public bool CanSupportNewTopBlock (Transform newTopBlock) {
        Transform topBlock = this.GetTopBlock ();
        if (!topBlock) {
            return true;
        }
        int blockNum = topBlock.GetComponent<Block> ().blockNum;
        int newBlockNum = newTopBlock.GetComponent<Block> ().blockNum;
        return blockNum > newBlockNum;
    }

    // BASIC METHODS-- FUNCTIONS THAT FREQUENTLY USED MORE ADVANCED FUNCTIONS
    // pushes newBlock parameter to stack and updates slot index accordingly
    public void PushTopBlock (Transform newBlock) {
        if (newBlock) {
            Transform slot = this.transform.GetChild (slotIndex);
            newBlock.SetParent (slot);
            newBlock.GetComponent<Block> ().ResetPosition ();
            slotIndex--;
        }
    }

    // pops top block from stack, updates slot index accordingly, and returns top block
    public Transform PopTopBlock () {
        if (slotIndex < maxTowerHeight - 1) {
            slotIndex++;
            Transform byeBlock = this.transform.GetChild (slotIndex).Find ("Block");
            return byeBlock;
        }
        return null;
    }

    // ADVANCED METHODS -- FUNCTIONS THAT PERFORM MULTIPLE TASKS USING COMPUTED PROPERTIES AND BASIC METHODS
    // examines top block from towerFrom parameter and transfers it to this tower's stack if move is valid
    public void AttemptBlockTransferFrom (Transform towerFrom) {
        if (towerFrom && towerFrom != this.transform) {
            TowerStack towerStackFrom = towerFrom.GetComponent<TowerStack> ();
            Transform topBlock = towerStackFrom.GetTopBlock ();

            if (topBlock && this.CanSupportNewTopBlock (topBlock)) {
                // block transfer is valid; commence transfer procedure
                topBlock = towerStackFrom.PopTopBlock ();
                this.PushTopBlock (topBlock);
            }
        }
    }

    // takes the newBlock parameter and assigns it as the bottom block of this tower
    public void GrowTowerFromBelow (Transform newBlock) {
        // acquire and reposition the slot
        Transform slot = this.transform.GetChild (0);
        slot.SetSiblingIndex (GameConstants.maxTowerHeight - 1);

        // place new block inside slot
        newBlock.transform.SetParent (slot);
        newBlock.GetComponent<Block> ().ResetPosition ();

        // adjust stack data
        this.slotIndex--;
    }

    // acquires the bottom block from, treats it as "gone" from the stack and returns it
    public Transform ChopTowerFromBelow () {
        Transform slot = this.transform.GetChild (maxTowerHeight - 1);
        if (slot.childCount > 0) {
            // remove block parentage and positioning
            slot.SetSiblingIndex (0);
            Transform choppedBlock = slot.Find ("Block");

            // adjust stack data
            this.slotIndex++;

            // return the chopped block
            return choppedBlock;
        }
        return null;
    }
}