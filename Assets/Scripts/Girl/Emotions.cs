using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    public Transform towerBindedTo;

    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite sadSprite;
    public Sprite glitchedSprite;

    public Sprite gainSprite;
    public Sprite lossSprite;
    public Sprite hopeSprite;
    public Sprite whineSprite;
    public Sprite curiousSprite;

    private Sprite defaultEmote;

    private Image image;
    private EventBus eventBus;

    void Start () {
        this.image = this.GetComponent<Image> ();

        this.eventBus = this.towerBindedTo.GetComponent<EventBus> ();
        this.eventBus.TopBlockGainEvent += HandleGain;
        this.eventBus.TopBlockLossEvent += HandleLoss;
        this.eventBus.EnterWhileDraggingEvent += HandleHope;
        this.eventBus.ExitWhileDraggingEvent += HandleWhine;
        this.eventBus.EnterNoDragEvent += HandleCurious;
        // this.eventBus.ExitNoDragEvent += HandleDefault;
        this.eventBus.TopBlockKeepEvent += HandleKeep;
        this.eventBus.TopBlockMissEvent += HandleLoss;

        this.image.sprite = defaultEmote = sadSprite;
    }

    public void RefreshDefaultEmote () {
        int towerHeight = this.towerBindedTo.GetComponent<TowerStack> ().GetTowerHeight ();
        if (towerHeight <= 1) {
            this.defaultEmote = sadSprite;
        } else if (towerHeight <= 4) {
            this.defaultEmote = neutralSprite;
        } else if (towerHeight <= maxTowerHeight) {
            this.defaultEmote = happySprite;
        } else {
            this.defaultEmote = glitchedSprite;
        }
    }

    void HandleGain (Transform tower) {
        StopAllCoroutines ();
        StartCoroutine (this.EmoteToGain ());
        this.RefreshDefaultEmote ();
    }
    IEnumerator EmoteToGain () {
        this.image.sprite = gainSprite;
        yield return new WaitForSeconds (1f);
        this.image.sprite = defaultEmote;
    }

    void HandleLoss (Transform tower) {
        // Debug.Log ("NOO! " + gameObject.name + " lost a block!");
        StopAllCoroutines ();
        StartCoroutine (this.EmoteToLoss ());
        this.RefreshDefaultEmote ();
    }
    IEnumerator EmoteToLoss () {
        this.image.sprite = lossSprite;
        yield return new WaitForSeconds (1f);
        this.image.sprite = defaultEmote;
    }

    void HandleHope (Transform tower) {
        StopAllCoroutines ();
        this.image.sprite = hopeSprite;
    }

    void HandleWhine (Transform tower) {
        StopAllCoroutines ();
        if (MouseDragTrigger.towerBeingDragged == tower) {
            // Debug.Log ("Oh no! " + gameObject.name + " might lose a block!");
            this.image.sprite = whineSprite;
        } else {
            this.image.sprite = defaultEmote;
        }
    }

    void HandleCurious (Transform tower) {
        StopAllCoroutines ();
        this.image.sprite = curiousSprite;
        // Debug.Log (":O " + gameObject.name + " is curious");
    }

    void HandleDefault (Transform fromTower) {
        this.image.sprite = defaultEmote;
    }

    void HandleKeep(Transform tower) {
        this.HandleGain(tower);
    }
}