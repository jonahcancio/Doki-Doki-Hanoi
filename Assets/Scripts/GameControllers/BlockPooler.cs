using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPooler : MonoBehaviour {

    public static int maxTowerHeight = 5;
    public GameObject blockPrefab;
    public GameObject slotPrefab;

    private Transform hanoiZone;
    [SerializeField]
    private static Stack<GameObject> pooledBlocks;

    void Start () {
        hanoiZone = GameObject.FindWithTag ("GameArea").transform;

        foreach (Transform tower in hanoiZone) {
            GameObject slot;
            for (int i = 0; i < maxTowerHeight; i++) {
                slot = Instantiate (slotPrefab, tower);
                slot.name = "Slot" + (i + 1);
            }
        }

        pooledBlocks = new Stack<GameObject> ();
        GameObject block;
        for (int i = maxTowerHeight; i > 0; i--) {
            block = Instantiate (blockPrefab, this.transform);
            block.name = "Block" + i;
            block.GetComponent<Block> ().blockNum = i;
            block.SetActive(false);
            pooledBlocks.Push (block);
        }
    }

    public static void PushBackToPool(GameObject block) {
        block.SetActive(false);
        pooledBlocks.Push(block);
    }

    public static GameObject PopOutFromPool() {
        GameObject block = pooledBlocks.Pop();
        block.SetActive(true);
        return block;
    }

}