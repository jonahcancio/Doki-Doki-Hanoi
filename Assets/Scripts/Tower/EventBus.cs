using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;

    private Action<Vector2> LeftMouseDragEvent;
    public Action LeftMouseReleaseEvent;
    public Action<Transform> TowerDropEvent;


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
}