using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTrigger : MonoBehaviour, IPointerClickHandler {

    // private float timeLastClicked;
    // private float timeLastDoubleClicked;
    private EventBus eventBus;

    void Start () {
        // this.timeLastClicked = Time.time;
        // this.timeLastDoubleClicked = Time.time;
        this.eventBus = this.GetComponent<EventBus> ();
    }

    public void OnPointerClick (PointerEventData eventData) {
        // if (eventData.button == PointerEventData.InputButton.Left) {
        //     float timeNow = Time.time;
        //     if (timeNow - this.timeLastClicked < 0.25f && timeNow - timeLastDoubleClicked > 0.5f) {
        //         timeLastDoubleClicked = timeNow;
        //         this.eventBus.EmitDoubleClickEvent (this.transform);
        //     }
        //     this.timeLastClicked = timeNow;
        // }
        if (eventData.button == PointerEventData.InputButton.Middle) {
            this.eventBus.EmitMiddleClickEvent (this.transform);
        }
        if (eventData.button == PointerEventData.InputButton.Right) {
            this.eventBus.EmitRightClickEvent ();
        }
    }

}