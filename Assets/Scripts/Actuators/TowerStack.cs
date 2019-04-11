using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStack : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    public int slotIndex;

    void Start () {
        this.slotIndex = maxTowerHeight - 1;
        for (int i = 0; i < maxTowerHeight; i++) {
            Transform slot = this.transform.GetChild (i);
            if (slot.childCount > 0) {
                this.slotIndex = i - 1;
                break;
            }
        }
    }

    public void PushTopBlock (Transform newBlock) {
        if (newBlock) {
            Transform slot = this.transform.GetChild (slotIndex);
            newBlock.SetParent (slot);
            newBlock.GetComponent<Block> ().ResetPosition ();
            slotIndex--;
        }
    }

    public Transform PopTopBlock () {
        if(slotIndex < maxTowerHeight - 1) {
            slotIndex++;
            Transform byeBlock = this.transform.GetChild(slotIndex).GetChild(0);
            return byeBlock;
        }
        return null;
    }

    public bool HasVacantSlot () {
        return this.slotIndex >= 0;
    }


    public Transform GetTopBlock() {
        int blockIndex = this.slotIndex + 1;
        if(blockIndex < maxTowerHeight) {
            return this.transform.GetChild(blockIndex).GetChild(0);
        }
        return null;
    }

}