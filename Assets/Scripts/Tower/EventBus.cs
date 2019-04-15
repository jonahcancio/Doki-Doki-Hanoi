using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;

    public Action<Vector2> LeftDragEvent;
    public void EmitLeftDragEvent (Vector2 mousePosition) {
        if (this.LeftDragEvent != null) {
            this.LeftDragEvent (mousePosition);
        }
    }

    public Action<Transform> LeftPressEvent;
    public void EmitLeftPressEvent (Transform towerPressed) {
        if (this.LeftPressEvent != null) {
            this.LeftPressEvent (towerPressed);
        }
    }

    public Action<Transform> LeftReleaseEvent;
    public void EmitLeftReleaseEvent (Transform fromTower) {
        if (this.LeftReleaseEvent != null) {
            this.LeftReleaseEvent (fromTower);
        }
    }

    public Action<Transform> TowerDropEvent;
    public void EmitTowerDropEvent (Transform towerFrom) {
        if (this.TowerDropEvent != null) {
            this.TowerDropEvent (towerFrom);
        }
    }

    public Action<Transform> MiddleClickEvent;
    public void EmitMiddleClickEvent (Transform towerClicked) {
        if (this.MiddleClickEvent != null) {
            this.MiddleClickEvent (towerClicked);
        }
    }


    public Action<Transform> RightClickEvent;
    public void EmitRightClickEvent (Transform towerClicked) {
        if (this.RightClickEvent != null) {
            this.RightClickEvent (towerClicked);
        }
    }

    public Action<Transform> EnterWhileDraggingEvent;
    public void EmitEnterWhileDraggingEvent (Transform toTower) {
        if (this.EnterWhileDraggingEvent != null) {
            this.EnterWhileDraggingEvent (toTower);
        }
    }

    public Action<Transform> ExitWhileDraggingEvent;
    public void EmitExitWhileDraggingEvent (Transform fromTower) {
        if (this.ExitWhileDraggingEvent != null) {
            this.ExitWhileDraggingEvent (fromTower);
        }
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

    public Action<Transform> TopBlockKeepEvent;
    public void EmitTopBlockKeepEvent(Transform keptTower) {
        if(this.TopBlockKeepEvent != null) {
            this.TopBlockKeepEvent(keptTower);
        }
    }

    public Action<Transform> TopBlockMissEvent;
    public void EmitTopBlockMissEvent(Transform missedTower) {
        if(this.TopBlockMissEvent != null) {
            this.TopBlockMissEvent(missedTower);
        }
    }

}