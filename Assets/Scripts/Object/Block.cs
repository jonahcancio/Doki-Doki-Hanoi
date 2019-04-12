using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Block : MonoBehaviour {
    private static int minBlockSize = 25;

    [RangeAttribute (1, 5)]
    public int blockNum = 1;

    [HideInInspector]
    public Vector2 originalLocalPosition;

    private Canvas canvas;
    private Image image;

    void OnEnable () {
        this.canvas = GetComponent<Canvas> ();
        this.image = GetComponent<Image> ();

        // initialize new instance of material on each block
        this.image.material = new Material(this.image.material);
    }

    public void Start () {
        this.SetSizeToBlockNum ();
        this.originalLocalPosition = this.transform.localPosition;
    }

    public void ResetPosition () {
        this.transform.localPosition = this.originalLocalPosition;
    }

    public void SetSizeToBlockNum () {
        RectTransform rectTransform = GetComponent<RectTransform> ();
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.x = minBlockSize * this.blockNum;
        rectTransform.sizeDelta = sizeDelta;
    }

    public void SetBlockNum (int blockNum) {
        this.blockNum = blockNum;
        this.SetSizeToBlockNum ();
    }

    public void SetColor (Color color) {
        this.image.color = color;
        // this.image.material.color = new Color(1f, 1f, 1f, 1f);
    }

    public void Shadowize() {
        Shader guiTextShader = Shader.Find("GUI/Text Shader");
        this.image.material.shader = guiTextShader;        
    }

    // public void Deshadowize() {
    //     Shader spriteDefault = Shader.Find("Sprites/Default");
    //     this.image.material.shader = spriteDefault;    
    // }
}