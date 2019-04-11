using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooler : MonoBehaviour {

    public static int maxTowerHeight = GameConstants.maxTowerHeight;

    public GameObject blockPrefab;
    public GameObject slotPrefab;

    private Transform hanoiZone;

    private static Stack<GameObject> pooledBlocks;


    void OnEnable () {
        // instantiate block slots
        this.hanoiZone = GameObject.FindWithTag ("GameArea").transform;
        foreach (Transform tower in this.hanoiZone) {
            GameObject slot;
            for (int i = 0; i < maxTowerHeight; i++) {
                slot = Instantiate (slotPrefab, tower);
                slot.name = "Slot" + (i + 1);
            }
        }

        // instantiate blocks
        pooledBlocks = new Stack<GameObject> ();
        GameObject block;
        for (int i = maxTowerHeight; i > 0; i--) {
            block = Instantiate (blockPrefab, this.transform);
            block.name = "Block" + i;
            block.GetComponent<Block> ().blockNum = i;
            block.SetActive (false);
            pooledBlocks.Push (block);
        }
    }

    public static void PushBackToPool (GameObject block) {
        block.SetActive (false);
        pooledBlocks.Push (block);
    }

    public static GameObject PopOutFromPool () {
        GameObject block = pooledBlocks.Pop ();
        block.SetActive (true);
        return block;
    }

    Transform GetTowerToChop () {
        Transform maxTower = this.hanoiZone.GetChild (0);
        foreach (Transform tower in this.hanoiZone) {
            int maxBlockNum = maxTower.GetComponent<TowerLogic> ().GetBottomBlockNum ();
            int blockNum = tower.GetComponent<TowerLogic> ().GetBottomBlockNum ();
            if (blockNum > maxBlockNum) {
                maxTower = tower;
            }
        }
        return maxTower;
    }

    public void ChopTower () {
        if (pooledBlocks.Count >= maxTowerHeight) {
            Debug.Log ("Nothing to chop, block pool is full");
        } else {
            // locate block from chop tower
            Transform towerToChop = GetTowerToChop ();
            // use tower logic to chop tower
            Transform block = towerToChop.GetComponent<TowerLogic> ().ChopTower ();
            if (block != null) {
                // push block back to stack
                block.SetParent (this.transform);
                block.gameObject.SetActive (false);
                pooledBlocks.Push (block.gameObject);
            }
        }
    }

    public void GrowTower (Transform towerToGrow) {
        if (pooledBlocks.Count <= 0) {
            Debug.Log ("No more pooled blocks left");
        } else {
            // pop block from stack
            GameObject block = pooledBlocks.Pop ();
            block.SetActive (true);

            // use tower logic to grow tower
            towerToGrow.GetComponent<TowerLogic> ().GrowTower (block.transform);
        }
    }
}