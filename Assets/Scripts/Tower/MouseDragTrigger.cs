using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDragTrigger : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IDropHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

        public static Transform towerBeingDragged;
        public static Transform towerReadyToDrop;
        public static Transform towerBelowMouse;

        private EventBus eventBus;

        public void Start () {

            towerBeingDragged = null;

            this.eventBus = this.GetComponent<EventBus> ();
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftPressEvent (this.transform);

                this.eventBus.EmitLeftMouseDragEvent (eventData.position);
                towerBeingDragged = this.transform;
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftMouseDragEvent (eventData.position);
            }
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftMouseReleaseEvent ();

                towerReadyToDrop = towerBeingDragged;
                towerBeingDragged = null;
            }
        }

        public void OnDrop (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitTowerDropEvent (towerReadyToDrop);
                towerReadyToDrop = null;
            }
        }

        public void OnPointerEnter (PointerEventData eventData) {
            towerBelowMouse = this.transform;
            if (towerBeingDragged) {
                this.eventBus.EmitEnterWhileDraggingEvent (this.transform, towerBeingDragged);
            }
        }

        public void OnPointerExit (PointerEventData eventData) {
            if (towerBeingDragged) {
                this.eventBus.EmitExitWhileDraggingEvent ();
            }
        }

        public void OnPointerClick (PointerEventData eventData) {            
            if (!towerBeingDragged) {
                if (eventData.button == PointerEventData.InputButton.Middle) {
                    this.eventBus.EmitMiddleClickEvent (this.transform);
                } else if (eventData.button == PointerEventData.InputButton.Right) {
                    this.eventBus.EmitRightClickEvent ();
                }
            }
        }
    }