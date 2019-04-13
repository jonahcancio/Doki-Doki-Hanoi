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
    public bool isFinishedDialoguing;

    // index used to traverse messages array
    private int messageIndex;

    // references from possible other gameObjects
    public Text nameText;
    public Text dialogueText;
    public Image hostImage;
    
    // saved triggers and defaults
    private Sprite defaultEmotion;
    private bool isTyping;
    private bool isDialoguing;
    
    private IEnumerator typingCoroutine;



    void Start () {
        this.isFinishedDialoguing = false;
        this.defaultEmotion = this.hostImage.sprite;
        this.StartDialogue ();        
    }

    public void StartDialogue () {
        this.messageIndex = -1;
        this.isDialoguing = true;
        this.isTyping = false;
        this.nameText.text = this.hostName;
        this.DisplayNextMessage ();
    }

    public void DisplayNextMessage () {
        this.messageIndex++;
        if (this.messageIndex >= this.messages.Length) {
            this.EndDialogue ();
            return;
        }
        this.typingCoroutine = TypeMessageByChar();
        StartCoroutine (this.typingCoroutine);
        Sprite sprite = this.messages[messageIndex].emotion;
        if (!sprite) {
             this.hostImage.sprite = this.defaultEmotion;
             return;
        }
        this.hostImage.sprite = this.messages[messageIndex].emotion;
    }

    public void FinishTypingMessage () {
        StopCoroutine (this.typingCoroutine);
        this.dialogueText.text = this.messages[messageIndex].text;
        this.isTyping = false;
    }

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

    void EndDialogue () {
        this.isDialoguing = false;
        this.nameText.text = this.defaultHeader;
        this.dialogueText.text = this.defaultInstructions;
        // GameController gameController = FindObjectOfType<GameController> ();
        // if (gameController) {
        //     gameController.TransitionToGame ();
        // }
        this.isFinishedDialoguing = true;
    }

    public void OnPointerClick (PointerEventData eventData) {
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