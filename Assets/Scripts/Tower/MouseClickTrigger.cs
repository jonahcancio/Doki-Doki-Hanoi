using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickTrigger : MonoBehaviour, IPointerClickHandler {

    public BlockPooler blockPooler;

    void Start() {
        this.blockPooler = GameObject.FindWithTag("GameController").GetComponent<BlockPooler>();
    }

    public void OnPointerClick (PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Middle) {
            blockPooler.GrowTower (this.transform);
        } else if (eventData.button == PointerEventData.InputButton.Right) {
            blockPooler.ChopTower ();
        }
    }

}