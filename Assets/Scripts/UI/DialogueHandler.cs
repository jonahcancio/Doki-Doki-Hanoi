using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour, IPointerClickHandler {
    // default dialogue texts
    private string defaultHeader = GameConstants.defaultHeader;
    // [TextArea (4, 10)]
    private string defaultInstructions = GameConstants.defaultInstructions;
    public string hostName = "Monika";

    // messages to be displayed
    public Message[] messages;

    // index used to traverse messages array
    private int messageIndex;

    // references from possible other gameObjects
    public Text nameText;
    public Text dialogueText;
    public Image hostImage;
    
    // host girl's default emotion
    private Sprite defaultEmotion;
    // whether or not typing is ongoing
    private bool isTyping;
    // whether or not dialogue is ongoing
    private bool isDialoguing;
    
    // coroutine that types each message letter by letter
    private IEnumerator typingCoroutine;

    void Start () {
        // start with host girl's default emotion
        this.defaultEmotion = this.hostImage.sprite;
        this.StartDialogue ();        
    }

    public void StartDialogue () {
        // initialize dialogue fields
        this.messageIndex = -1;
        this.isDialoguing = true;
        this.isTyping = false;
        this.nameText.text = this.hostName;
        // display first message
        this.DisplayNextMessage ();
    }

    // displays the next message in the messages array
    public void DisplayNextMessage () {
        this.messageIndex++;
        if (this.messageIndex >= this.messages.Length) {
            // end dialogue if end of messages list reached
            this.EndDialogue ();
            return;
        }

        // begin typing the message letter by letter
        this.typingCoroutine = TypeMessageByChar();
        StartCoroutine (this.typingCoroutine);
        Sprite sprite = this.messages[messageIndex].emotion;
        if (!sprite) {
             this.hostImage.sprite = this.defaultEmotion;
             return;
        }
        this.hostImage.sprite = this.messages[messageIndex].emotion;
    }

    // immediately finish typing the message
    public void FinishTypingMessage () {
        StopCoroutine (this.typingCoroutine);
        this.dialogueText.text = this.messages[messageIndex].text;
        this.isTyping = false;
    }

    // coroutine for typing message letter by letter
    IEnumerator TypeMessageByChar () {
        this.isTyping = true;
        this.dialogueText.text = "";
        string messageText = this.messages[messageIndex].text;
        foreach (char letter in messageText.ToCharArray ()) {
            this.dialogueText.text += letter;
            yield return null;
        }
        this.isTyping = false;
    }

    // end dialogue
    void EndDialogue () {
        // end dialogue fields
        this.isDialoguing = false;
        this.nameText.text = this.defaultHeader;
        this.dialogueText.text = this.defaultInstructions;

        // emit dialogue finished
        this.EmitDialogueFinishedEvent();
    }

    // dialogue event that game controller has subscribed to
    public Action DialogueFinishedEvent;
    void EmitDialogueFinishedEvent() {
        if(this.DialogueFinishedEvent != null) {
            this.DialogueFinishedEvent();
        }
    }

    // click sensor for progressing dialogue
    public void OnPointerClick (PointerEventData eventData) {
        // only detect left clicks
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!this.isDialoguing) {
                this.StartDialogue ();
            } else {
                if (this.isTyping) {
                    this.FinishTypingMessage ();
                } else {
                    this.DisplayNextMessage();
                }
            }
        }
    }
}