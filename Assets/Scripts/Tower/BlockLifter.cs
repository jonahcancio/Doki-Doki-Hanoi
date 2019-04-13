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
        this.eventBus.OnLeftMouseDragEvent(this.MoveTopBlockToMouse);
        this.eventBus.OnLeftMouseReleaseEvent(this.ResetTopBlockPosition);
        // this.eventBus.OnMiddleClickMouseEvent += this.MoveTopBlockToMouse;
        // this.eventBus.OnRightClickEvent += this.MoveTopBlockToMouse;
    }

    public void MoveTopBlockToMouse (Vector2 mousePosition) {
        Transform topBlock = this.towerStack.GetTopBlock();
        if (topBlock) {
            Vector2 world = this.mainCamera.ScreenToWorldPoint (mousePosition);
            topBlock.position = new Vector3 (world.x, world.y, mainCanvas.planeDistance);
        } else{
            Debug.Log("No top block to move to mouse");
        }
    }

    public void ResetTopBlockPosition() {
        Transform topBlock = this.towerStack.GetTopBlock();
        if (topBlock) {
            topBlock.GetComponent<Block> ().ResetPosition ();
        }        
    }
}