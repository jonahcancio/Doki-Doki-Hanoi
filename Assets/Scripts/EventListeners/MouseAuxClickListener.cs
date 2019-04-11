using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseAuxClickListener : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick (PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Middle) {
            GameObject block = BlockPooler.PopOutFromPool ();
        } else if (eventData.button ==  PointerEventData.InputButton.Right) {

        }
    }
}