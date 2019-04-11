using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (TowerHandler))]
public class MouseDragListener : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerUpHandler {

        public static Transform towerBeingDragged;

        private Image image;
        private Color originalColor;
        public Color selectedColor = new Color(1f, 2f, 0f, 0.5f);

        private TowerHandler towerHandler;

        public void Start () {
            this.image = this.GetComponent<Image> ();
            this.originalColor = this.image.color;

            this.towerHandler = this.GetComponent<TowerHandler> ();
            towerBeingDragged = null;
        }

        public void OnPointerDown (PointerEventData eventData) {
            if (eventData.button == 0) {
                towerHandler.MoveTopBlockToMouse (eventData.position);

                towerBeingDragged = this.transform;
                this.image.color = this.selectedColor;
            }
        }

        public void OnDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                towerHandler.MoveTopBlockToMouse (eventData.position);
            }
        }

        public void OnPointerUp (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerHandler.ResetTopBlockPosition ();

                this.image.color = this.originalColor;
            }
        }

        public void OnDrop (PointerEventData eventData) {
            if (eventData.button == 0) {
                if (towerBeingDragged && towerBeingDragged != this.transform) {                    
                    towerHandler.AttemptTopBlockTheftFromDraggedTower ();
                }
            }
        }

        public void OnEndDrag (PointerEventData eventData) {
            if (eventData.button == 0) {
                this.towerHandler.ResetTopBlockPosition ();

                towerBeingDragged = null;
                this.image.color = this.originalColor;                
            }
        }


    }