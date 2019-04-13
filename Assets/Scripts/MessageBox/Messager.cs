using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messager : MonoBehaviour
{
    public string defaultSpeaker = "Monika";
    [TextArea(4,12)]
    public string defaultInstructions = 
    @"Controls:
        Left Click – drag and drop tower blocks
        Middle Click – add extra tower blocks
        Right Click – remove biggest tower blocks
    Objective:
        Stack as many tower blocks on the girl of your choice to make her happy. 
        Bigger blocks CANNOT be placed on top of smaller blocks. 
    ";

    private Text nameText;
    private Text messageText;
    
    void OnEnable() {
        this.nameText = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        this.messageText = this.transform.GetChild(1).GetComponent<Text>();
    }


    public void SetNameText(string name) {
        this.nameText.text = name;
    }

    public void SetMessageText(string message) {
        this.messageText.text = message;
    }

    public void ResetMessagePanel() {
        this.nameText.text = this.defaultSpeaker;
        this.messageText.text = this.defaultInstructions;
    }
}
