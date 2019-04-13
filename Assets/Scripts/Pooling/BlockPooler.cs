using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooler : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    private int initialBlockCount = GameConstants.initialBlockCount;

    public GameObject blockPrefab;
    public GameObject slotPrefab;

    public Transform gameArea;

    private static Stack<GameObject> pooledBlocks;

    public Transform initialTower;

    void OnEnable () {
        // instantiate block slots
        if (!this.gameArea) {
            this.gameArea = GameObject.FindWithTag ("GameArea").transform;
        }
        foreach (Transform tower in this.gameArea) {
            GameObject slot;
            for (int i = 0; i < maxTowerHeight; i++) {
                slot = Instantiate (slotPrefab, tower);
                slot.name = "Slot" + (i + 1);
            }
            tower.GetComponent<TowerStack> ().RefreshSlotIndex ();
        }

        // instantiate blocks
        pooledBlocks = new Stack<GameObject> ();
        GameObject block;
        for (int i = maxTowerHeight; i > 0; i--) {
            block = Instantiate (blockPrefab, this.transform);
            block.name = "Block";
            block.GetComponent<Block> ().blockNum = i;
            block.SetActive (false);
            pooledBlocks.Push (block);
        }
    }

    void Start () {
        // setup initial tower
        if (!this.initialTower) {
            initialTower = GameObject.FindWithTag ("InitialTower").transform;
        }

        ResetBlocksToInitialTower ();

        // subscribe chop and grow tower events to middle and right mouse clicks
        foreach (Transform tower in this.gameArea) {
            EventBus eventBus = tower.GetComponent<EventBus> ();
            eventBus.OnMiddleClickEvent (this.GrowTower);
            eventBus.OnRightClickEvent (this.ChopTower);
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

    public void ChopTower () {
        if (pooledBlocks.Count >= maxTowerHeight) {
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
            pooledBlocks.Push (block.gameObject);
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
            // towerToGrow.GetComponent<TowerStack> ().GrowTowerFromBelow (block.transform);
            initialTower.GetComponent<TowerStack> ().GrowTowerFromBelow (block.transform);
        }
    }

    public void ResetBlocksToInitialTower () {
        while (pooledBlocks.Count < maxTowerHeight) {
            this.ChopTower ();
        }
        for (int i = 0; i < initialBlockCount; i++) {
            this.GrowTower (null);
        }
    }
}