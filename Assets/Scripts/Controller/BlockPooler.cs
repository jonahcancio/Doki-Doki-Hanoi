using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooler : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    private int initialBlockCount = GameConstants.initialBlockCount;

    public GameObject blockPrefab;
    public GameObject slotPrefab;

    private Transform hanoiZone;

    private static Stack<GameObject> pooledBlocks;

    private Transform initialTower;

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
            block.name = "Block";
            block.GetComponent<Block> ().blockNum = i;
            block.SetActive (false);
            pooledBlocks.Push (block);
        }

        // setup initial tower
        initialTower = GameObject.FindWithTag("InitialTower").transform;
        for (int i = 0; i < initialBlockCount; i++) {
            this.GrowTower(null);
        }
    }

    void Start() {
        // subscribe chop and grow tower events to middle and right mouse clicks
        foreach(Transform tower in this.hanoiZone) {
            EventBus eventBus = tower.GetComponent<EventBus>();
            eventBus.OnMiddleClickEvent(this.GrowTower);
            eventBus.OnRightClickEvent(this.ChopTower);
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
}