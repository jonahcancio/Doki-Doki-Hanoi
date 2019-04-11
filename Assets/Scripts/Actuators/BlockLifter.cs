using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLifter : MonoBehaviour {

    private Camera mainCamera;
    private Canvas mainCanvas;

    private TowerLogic towerLogic;

    void Start() {
        this.mainCamera = Camera.main;
        this.mainCanvas = this.transform.parent.GetComponent<Canvas> ().rootCanvas;

        // subscribe methods to mouse events
        this.towerLogic = this.GetComponent<TowerLogic>();
        this.towerLogic.OnLeftMouseDragEvent(this.MoveTopBlockToMouse);
        this.towerLogic.OnLeftMouseReleaseEvent(this.ResetTopBlockPosition);
    }

    public void MoveTopBlockToMouse (Vector2 mousePosition) {
        Transform topBlock = this.towerLogic.GetTopBlock();
        if (topBlock) {
            Vector2 world = this.mainCamera.ScreenToWorldPoint (mousePosition);
            topBlock.position = new Vector3 (world.x, world.y, mainCanvas.planeDistance);
        } else{
            Debug.Log("No top block to move to mouse");
        }
    }

    public void ResetTopBlockPosition() {
        Transform topBlock = this.towerLogic.GetTopBlock();
        if (topBlock) {
            topBlock.GetComponent<Block> ().ResetPosition ();
        }        
    }
}