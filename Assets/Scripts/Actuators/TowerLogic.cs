using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLogic : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    private TowerStack towerStack;

    private Action<Vector2> LeftMouseDragEvent;
    public Action LeftMouseReleaseEvent;

    void OnEnable () {
        this.towerStack = this.GetComponent<TowerStack> ();
    }

    public Transform GetTopBlock () {
        return this.towerStack.GetTopBlock ();
    }

    public int GetTowerHeight() {
        return this.maxTowerHeight - this.towerStack.slotIndex - 1;
    }

    bool CanSupportNewTopBlock (Transform newTopBlock) {
        Transform topBlock = this.GetTopBlock ();
        if (!topBlock) {
            return true;
        }
        int blockNum = topBlock.GetComponent<Block> ().blockNum;
        int newBlockNum = newTopBlock.GetComponent<Block> ().blockNum;
        return blockNum > newBlockNum;
    }

    public int GetBottomBlockNum () {
        Transform bottomSlot = this.transform.GetChild (maxTowerHeight - 1);
        if (bottomSlot.childCount > 0) {
            Transform bottomBlock = bottomSlot.GetChild (0);
            if (bottomBlock) {
                return bottomBlock.GetComponent<Block> ().blockNum;
            }
        }
        return 0;
    }

    public void AttemptBlockTransferFrom (Transform towerFrom) {
        if (towerFrom && towerFrom != this.transform) {
            TowerStack towerStackFrom = towerFrom.GetComponent<TowerStack> ();
            Transform topBlock = towerStackFrom.GetTopBlock ();

            if (topBlock && this.CanSupportNewTopBlock (topBlock)) {
                // block transfer is valid; commence transfer procedure
                topBlock = towerStackFrom.PopTopBlock ();
                this.towerStack.PushTopBlock (topBlock);
            }
        }
    }

    public void GrowTower (Transform block) {
        // adjust block to appropriate transform
        Transform slot = this.transform.GetChild (0);
        slot.SetSiblingIndex (GameConstants.maxTowerHeight - 1);
        block.transform.SetParent (slot);
        block.GetComponent<Block> ().ResetPosition ();

        // adjust grown tower's TowerStack data
        this.towerStack.slotIndex--;

    }

    public Transform ChopTower () {
        Transform slot = this.transform.GetChild (maxTowerHeight - 1);
        if (slot.childCount > 0) {
            // remove block parentage and positioning
            slot.SetSiblingIndex (0);
            Transform block = slot.GetChild (0);

            // adjust chopped tower's TowerStack data
            towerStack.slotIndex++;
            return block;
        }
        return null;
    }

    public void EmitLeftMouseDragEvent (Vector2 mousePosition) {
        if (this.LeftMouseDragEvent != null) {
            this.LeftMouseDragEvent (mousePosition);
        }
    }
    public void OnLeftMouseDragEvent (Action<Vector2> callback) {
        this.LeftMouseDragEvent += callback;
    }

    public void EmitLeftMouseReleaseEvent () {
        if (this.LeftMouseReleaseEvent != null) {
            this.LeftMouseReleaseEvent ();
        }
    }
    public void OnLeftMouseReleaseEvent (Action callback) {
        this.LeftMouseReleaseEvent += callback;
    }
}