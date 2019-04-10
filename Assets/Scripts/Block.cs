using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private static int minBlockSize = 25;

    [RangeAttribute(1,5)]
    public int blockNum = 1;
    
    [HideInInspector]
    public Vector2 originalLocalPosition;

    public void Start() {        
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = minBlockSize * this.blockNum;
        rectTransform.sizeDelta = sizeDelta;

        this.originalLocalPosition = this.transform.localPosition;
    }

    public void ResetPosition() {
        this.transform.localPosition = this.originalLocalPosition;
    }

    // [ContextMenu("Zoom Canvas")]
    // public void ZoomCanvas() {
    //     Canvas root = GetComponent<Canvas>().rootCanvas;
    //     root.scaleFactor = 2f;
    // }
}
