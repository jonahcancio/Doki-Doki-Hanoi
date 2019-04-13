using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;

    public Action<Vector2> LeftMouseDragEvent;
    public Action LeftMouseReleaseEvent;
    public Action<Transform> TowerDropEvent;
    public Action<Transform> MiddleClickEvent;
    public Action<Vector2> MiddleClickMouseEvent;
    public Action RightClickEvent;
    public Action<Vector2> RightClickMouseEvent;

    public Action<Transform, Transform> EnterWhileDraggingEvent;
    public Action ExitWhileDraggingEvent;
    public Action<Transform> LeftPressEvent;

    // Binding methods for LeftMouseDragEvent
    public void EmitLeftMouseDragEvent (Vector2 mousePosition) {
        if (this.LeftMouseDragEvent != null) {
            this.LeftMouseDragEvent (mousePosition);
        }
    }
    public void OnLeftMouseDragEvent (Action<Vector2> callback) {
        this.LeftMouseDragEvent += callback;
    }

    // Binding methods for LeftMouseReleaseEvent
    public void EmitLeftMouseReleaseEvent () {
        if (this.LeftMouseReleaseEvent != null) {
            this.LeftMouseReleaseEvent ();
        }
    }
    public void OnLeftMouseReleaseEvent (Action callback) {
        this.LeftMouseReleaseEvent += callback;
    }

    // Binding methods for TowerDropEvent
    public void EmitTowerDropEvent (Transform towerFrom) {
        if (this.TowerDropEvent != null) {
            this.TowerDropEvent (towerFrom);
        }
    }
    public void OnTowerDropEvent (Action<Transform> callback) {
        this.TowerDropEvent += callback;
    }

    // Binding methods for MiddleClickEvent
    public void EmitMiddleClickEvent (Transform towerClicked) {
        if (this.MiddleClickEvent != null) {
            this.MiddleClickEvent (towerClicked);
        }
    }
    public void OnMiddleClickEvent (Action<Transform> callback) {
        this.MiddleClickEvent += callback;
    }

    // Binding methods for RightClickEvent
    public void EmitRightClickEvent () {
        if (this.RightClickEvent != null) {
            this.RightClickEvent ();
        }
    }
    public void OnRightClickEvent (Action callback) {
        this.RightClickEvent += callback;
    }

    public void EmitEnterWhileDraggingEvent (Transform toTower, Transform fromTower) {
        if (this.EnterWhileDraggingEvent != null) {
            this.EnterWhileDraggingEvent (toTower, fromTower);
        }
    }
    public void OnEnterWhileDraggingEvent (Action<Transform, Transform> callback) {
        this.EnterWhileDraggingEvent += callback;
    }

    public void EmitExitWhileDraggingEvent () {
        if (this.ExitWhileDraggingEvent != null) {
            this.ExitWhileDraggingEvent ();
        }
    }
    public void OnExitWhileDraggingEvent (Action callback) {
        this.ExitWhileDraggingEvent += callback;
    }

    public void EmitLeftPressEvent (Transform towerPressed) {
        if (this.LeftPressEvent != null) {
            this.LeftPressEvent (towerPressed);
        }
    }
    public void OnLeftPressEvent (Action<Transform> callback) {
        this.LeftPressEvent += callback;
    }

    // public void EmitMiddleClickMouseEvent (Vector2 mousePosition) {
    //     if (this.MiddleClickEvent != null) {
    //         this.MiddleClickEvent (mousePosition);
    //     }
    // }
}