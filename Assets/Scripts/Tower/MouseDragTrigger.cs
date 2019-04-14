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
        public static Transform topBlockBeingDragged;
        public static Transform towerUnderPointer;

        private EventBus eventBus;

        public void Start () {

            towerBeingDragged = null;

            this.eventBus = this.GetComponent<EventBus> ();
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftPressEvent (this.transform);

                this.eventBus.EmitLeftDragEvent (eventData.position);
                towerBeingDragged = this.transform;
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftDragEvent (eventData.position);
            }
        }

        // called first before onDrop and is called on tower mouse was first pressed regardless of drag
        public void OnPointerUp (PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                this.eventBus.EmitLeftReleaseEvent (this.transform);

                // if block was dragged around but stays within tower bounds
                if(topBlockBeingDragged && towerUnderPointer == this.transform && towerBeingDragged == this.transform) {
                    this.eventBus.EmitTopBlockKeepEvent(this.transform);
                }
                topBlockBeingDragged = null;

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
            towerUnderPointer = this.transform;
            if (towerBeingDragged) {
                this.eventBus.EmitEnterWhileDraggingEvent (this.transform);
            } else {
                this.eventBus.EmitEnterNoDragEvent(this.transform);
            }
        }

        public void OnPointerExit (PointerEventData eventData) {
            towerUnderPointer = null;
            if (towerBeingDragged) {
                this.eventBus.EmitExitWhileDraggingEvent (this.transform);
            }else {
                this.eventBus.EmitExitNoDragEvent(this.transform);
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