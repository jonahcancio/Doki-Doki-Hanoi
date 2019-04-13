using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTrigger : MonoBehaviour, IPointerClickHandler {

    private EventBus eventBus;

    void Start () {

        this.eventBus = this.GetComponent<EventBus> ();
    }

    public void OnPointerClick (PointerEventData eventData) {
        if (!MouseDragTrigger.towerBeingDragged) {
            if (eventData.button == PointerEventData.InputButton.Middle) {
                this.eventBus.EmitMiddleClickEvent (this.transform);
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                this.eventBus.EmitRightClickEvent ();
            }
        }
    }

}