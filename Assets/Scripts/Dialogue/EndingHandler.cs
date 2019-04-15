using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// used to edit text and sprite in the ending
public class EndingHandler : MonoBehaviour {

    public Text bestGirlRemark;
    public Text bestGirlName;
    public Image bestGirlImage;

    public void SetBestGirlRemark(string remark) {
        this.bestGirlRemark.text = remark;
    }

    public void SetBestGirlName(string name) {
        this.bestGirlName.text = "Your best girl is " + name + ".";
    }

    public void SetBestGirlSprite(Sprite sprite) {
        this.bestGirlImage.sprite = sprite;
    }
}