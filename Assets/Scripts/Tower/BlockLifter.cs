using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLifter : MonoBehaviour {

    private Camera mainCamera;
    private Canvas mainCanvas;

    private EventBus eventBus;
    private TowerStack towerStack;

    void Start() {
        this.mainCamera = Camera.main;
        this.mainCanvas = this.transform.parent.GetComponent<Canvas> ().rootCanvas;

        this.towerStack = this.GetComponent<TowerStack>();

        // subscribe methods to event bus
        this.eventBus = this.GetComponent<EventBus>();
        this.eventBus.LeftDragEvent += this.MoveTopBlockToMouse;
        this.eventBus.LeftReleaseEvent += this.ResetTopBlockPosition;
    }

    // used to move topblock to mouse position as it drags it
    public void MoveTopBlockToMouse (Vector2 mousePosition) {
        Transform topBlock = this.towerStack.GetTopBlock();
        if (topBlock) {
            MouseActionTrigger.topBlockBeingDragged = topBlock;
            // convert mouse position to world coordinates
            Vector2 world = this.mainCamera.ScreenToWorldPoint (mousePosition);
            // move topblock's position to mouse's world coordinate's position at the same z-axis as main canvas
            topBlock.position = new Vector3 (world.x, world.y, mainCanvas.planeDistance);            
        }
    }

    // used to release top block back to its original position when mouse releases
    public void ResetTopBlockPosition(Transform fromTower) {
        Transform topBlock = this.towerStack.GetTopBlock();
        if (topBlock) {
            topBlock.GetComponent<Block> ().ResetPosition ();
        }        
    }
}