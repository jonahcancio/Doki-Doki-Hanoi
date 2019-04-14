using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;

    public Action<Vector2> LeftDragEvent;
    // Binding methods for LeftDragEvent
    public void EmitLeftDragEvent (Vector2 mousePosition) {
        if (this.LeftDragEvent != null) {
            this.LeftDragEvent (mousePosition);
        }
    }
    public void OnLeftDragEvent (Action<Vector2> callback) {
        this.LeftDragEvent += callback;
    }

    public Action<Transform> LeftPressEvent;
    // Binding methods for LeftPress
    public void EmitLeftPressEvent (Transform towerPressed) {
        if (this.LeftPressEvent != null) {
            this.LeftPressEvent (towerPressed);
        }
    }
    public void OnLeftPressEvent (Action<Transform> callback) {
        this.LeftPressEvent += callback;
    }

    public Action<Transform> LeftReleaseEvent;
    // Binding methods for LeftReleaseEvent
    public void EmitLeftReleaseEvent (Transform fromTower) {
        if (this.LeftReleaseEvent != null) {
            this.LeftReleaseEvent (fromTower);
        }
    }
    public void OnLeftReleaseEvent (Action<Transform> callback) {
        this.LeftReleaseEvent += callback;
    }

    public Action<Transform> TowerDropEvent;
    // Binding methods for TowerDropEvent
    public void EmitTowerDropEvent (Transform towerFrom) {
        if (this.TowerDropEvent != null) {
            this.TowerDropEvent (towerFrom);
        }
    }
    public void OnTowerDropEvent (Action<Transform> callback) {
        this.TowerDropEvent += callback;
    }

    public Action<Transform> MiddleClickEvent;
    // Binding methods for MiddleClickEvent
    public void EmitMiddleClickEvent (Transform towerClicked) {
        if (this.MiddleClickEvent != null) {
            this.MiddleClickEvent (towerClicked);
        }
    }
    public void OnMiddleClickEvent (Action<Transform> callback) {
        this.MiddleClickEvent += callback;
    }

    public Action RightClickEvent;
    // Binding methods for RightClickEvent
    public void EmitRightClickEvent () {
        if (this.RightClickEvent != null) {
            this.RightClickEvent ();
        }
    }
    public void OnRightClickEvent (Action callback) {
        this.RightClickEvent += callback;
    }

    public Action<Transform> EnterWhileDraggingEvent;
    // Binding methods for EnterWhileDragging
    public void EmitEnterWhileDraggingEvent (Transform toTower) {
        if (this.EnterWhileDraggingEvent != null) {
            this.EnterWhileDraggingEvent (toTower);
        }
    }
    public void OnEnterWhileDraggingEvent (Action<Transform> callback) {
        this.EnterWhileDraggingEvent += callback;
    }

    public Action<Transform> ExitWhileDraggingEvent;
    // Binding methods for ExitWhileDragging
    public void EmitExitWhileDraggingEvent (Transform fromTower) {
        if (this.ExitWhileDraggingEvent != null) {
            this.ExitWhileDraggingEvent (fromTower);
        }
    }
    public void OnExitWhileDraggingEvent (Action<Transform> callback) {
        this.ExitWhileDraggingEvent += callback;
    }

    public Action<Transform> TopBlockGainEvent;
    public void EmitTopBlockGainEvent (Transform gainedTower) {
        if (this.TopBlockGainEvent != null) {
            this.TopBlockGainEvent (gainedTower);
        }
    }

    public Action<Transform> TopBlockLossEvent;
    public void EmitTopBlockLossEvent (Transform lostTower) {
        if (this.TopBlockLossEvent != null) {
            this.TopBlockLossEvent (lostTower);
        }
    }

    public Action<Transform> EnterNoDragEvent;
    public void EmitEnterNoDragEvent (Transform hoveredTower) {
        if (this.EnterNoDragEvent != null) {
            this.EnterNoDragEvent (hoveredTower);
        }
    }

    public Action<Transform> ExitNoDragEvent;
    public void EmitExitNoDragEvent (Transform exitedTower) {
        if (this.ExitNoDragEvent != null) {
            this.ExitNoDragEvent (exitedTower);
        }
    }

}