using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour, IPointerClickHandler {

    public Dialogue dialog;
    private DialogueManager dialogueManager;

    void Start () {
        this.dialogueManager = FindObjectOfType<DialogueManager> ();
        dialogueManager.StartDialogue (this.dialog);
    }

    public void OnPointerClick (PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) {
            if (!dialogueManager.isDialoging) {
                dialogueManager.StartDialogue (this.dialog);
            } else {
                dialogueManager.DisplayNextMessage ();
            }
        }
    }
}