using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDragTrigger : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerUpHandler,
    IPointerEnterHandler, IPointerExitHandler {

        public static Transform towerBeingDragged;
        public static Transform towerBelowMouse;

        // private Image image;
        // private Color originalColor;
        // public Color selectedColor = new Color (1f, 2f, 0f, 0.5f);

        private EventBus eventBus;

        public void Start () {
            // this.image = this.GetComponent<Image> ();
            // this.originalColor = this.image.color;

            towerBeingDragged = null;

            this.eventBus = this.GetComponent<EventBus> ();
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.eventBus.EmitLeftPressEvent (this.transform);

                this.eventBus.EmitLeftMouseDragEvent (eventData.position);
                towerBeingDragged = this.transform;
                // this.image.color = this.selectedColor;
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.eventBus.EmitLeftMouseDragEvent (eventData.position);
            }
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.eventBus.EmitLeftMouseReleaseEvent ();

                // this.image.color = this.originalColor;
            }
        }

        public void OnDrop (PointerEventData eventData) {
            if (eventData.button == 0) {
                if (towerBeingDragged) {
                    this.eventBus.EmitTowerDropEvent (towerBeingDragged);
                }
            }
        }

        public void OnEndDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.eventBus.EmitLeftMouseReleaseEvent ();

                towerBeingDragged = null;
                // this.image.color = this.originalColor;
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

    }