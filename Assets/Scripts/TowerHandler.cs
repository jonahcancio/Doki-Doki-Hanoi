using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour {

    static private int maxTowerHeight = 5;

    public int towerHeight;
    public Transform topBlock;

    void Start () {
        this.towerHeight = 0;
        this.topBlock = null;
        for (int i = 0; i < maxTowerHeight; i++) {
            Transform slot = this.transform.GetChild (i);
            if (slot.childCount > 0) {
                this.towerHeight = maxTowerHeight - i;
                this.topBlock = slot.GetChild (0);
                break;
            }
        }
    }

    Transform GetAvailableSlot () {
        int index = maxTowerHeight - this.towerHeight - 1;
        if (this.towerHeight < maxTowerHeight) {
            return this.transform.GetChild (index);
        } else {
            return null;
        }
    }

    bool CanSupportNewTopBlock (Transform newTopBlock) {
        if (!this.topBlock) {
            return true;
        }
        int blockNum = this.topBlock.GetComponent<Block> ().blockNum;
        int newBlockNum = newTopBlock.GetComponent<Block> ().blockNum;
        return blockNum > newBlockNum;
    }

    void RefreshTopBlockInstance () {
        int index = maxTowerHeight - this.towerHeight;
        if (this.towerHeight > 0) {
            this.topBlock = this.transform.GetChild (index).GetChild (0);
        } else {
            this.topBlock = null;
        }
        this.ResetTopBlockPosition ();
    }

    public void MoveTopBlockToMouse (Vector2 mousePosition) {
        if (this.topBlock) {
            this.topBlock.position = mousePosition;
        }
    }

    public void ResetTopBlockPosition () {
        if (this.topBlock) {
            this.topBlock.GetComponent<Block> ().ResetPosition ();
        }
    }

    public void AttemptTopBlockTheftFromDraggedTower () {
        TowerHandler draggedTowerHandler = MouseDragListener.towerBeingDragged.GetComponent<TowerHandler> ();
        Transform draggedTopBlock = draggedTowerHandler.topBlock;
        if (draggedTopBlock) {
            Transform availableSlot = this.GetAvailableSlot ();
            if (availableSlot && this.CanSupportNewTopBlock (draggedTopBlock)) {
                draggedTopBlock.SetParent (availableSlot);
                this.towerHeight++;
                this.RefreshTopBlockInstance ();

                draggedTowerHandler.towerHeight--;
                draggedTowerHandler.RefreshTopBlockInstance ();
            }
        }
    }
}