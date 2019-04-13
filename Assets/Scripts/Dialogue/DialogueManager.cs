using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> messages;
    public bool isDialoging;
    public Messager messager;

    void Start()
    {
        this.messages = new Queue<string>();
        this.isDialoging = false;

        this.messager = GameObject.FindWithTag("DialogueBox").GetComponent<Messager>();
    }

    public void StartDialogue(Dialogue dialog) {
        this.messages.Clear();

        foreach (string message in dialog.messages) {
            this.messages.Enqueue(message);
        }

        this.isDialoging = true;
        this.messager.SetNameText(dialog.name);
        this.DisplayNextMessage();
    }

    public void DisplayNextMessage() {
        if (messages.Count <= 0) {
            EndDialogue();
            return;
        }

        string message = messages.Dequeue();
        // this.messager.SetMessageText(message);
        StopAllCoroutines();
        StartCoroutine(TypeMessageByChar(message));
    }

    IEnumerator TypeMessageByChar(string message) {
        string currentMessage = "";
        this.messager.SetMessageText(currentMessage);
        foreach (char letter in message.ToCharArray()) {
            currentMessage += letter;
            this.messager.SetMessageText(currentMessage);
            yield return null;
        }
    }

    void EndDialogue() {
        isDialoging = false;
        this.messager.ResetMessagePanel();
        FindObjectOfType<GameController> ().TransitionToGame();
    }
}
