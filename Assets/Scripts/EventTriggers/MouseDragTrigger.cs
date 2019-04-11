using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseDragTrigger : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerUpHandler {

        public static Transform towerBeingDragged;

        private Image image;
        private Color originalColor;
        public Color selectedColor = new Color (1f, 2f, 0f, 0.5f);

        private TowerLogic towerLogic;

        public void Start () {
            this.image = this.GetComponent<Image> ();
            this.originalColor = this.image.color;

            towerBeingDragged = null;

            this.towerLogic = this.GetComponent<TowerLogic> ();
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerLogic.EmitLeftMouseDragEvent (eventData.position);

                towerBeingDragged = this.transform;
                this.image.color = this.selectedColor;
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerLogic.EmitLeftMouseDragEvent (eventData.position);
            }
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerLogic.EmitLeftMouseReleaseEvent();

                this.image.color = this.originalColor;
            }
        }

        public void OnDrop (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerLogic.AttemptBlockTransferFrom(towerBeingDragged);
            }
        }

        public void OnEndDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerLogic.EmitLeftMouseReleaseEvent();

                towerBeingDragged = null;
                this.image.color = this.originalColor;
            }
        }
    }