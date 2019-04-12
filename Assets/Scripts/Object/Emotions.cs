using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    public TowerStack towerBindedTo;

    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    public Sprite glitchedSprite;

    private int previousHeight;

    private Image image;

    void Start () {
        this.image = this.GetComponent<Image> ();
        this.previousHeight = this.towerBindedTo.GetTowerHeight ();
    }

    void Update () {
        int towerHeight = this.towerBindedTo.GetTowerHeight ();
        if (towerHeight != this.previousHeight) {
            if (towerHeight <= 1) {
                this.image.sprite = sadSprite;
            } else if (towerHeight <= 4) {
                this.image.sprite = neutralSprite;
            } else if (towerHeight <= maxTowerHeight) {
                this.image.sprite = happySprite;
            } else {
                this.image.sprite = glitchedSprite;
            }
        }
        this.previousHeight = towerHeight;
    }
}