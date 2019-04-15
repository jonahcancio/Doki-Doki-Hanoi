using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour {

    private int maxTowerHeight = GameConstants.maxTowerHeight;
    public Transform towerBindedTo;

    // default sprite when tower is high
    public Sprite happySprite;
    // default sprite when tower is neutral
    public Sprite neutralSprite;
    // default sprite when tower is low
    public Sprite sadSprite;

    // debugging sprite: will only show up on error
    public Sprite glitchedSprite;

    // sprite to emote to when just gained or kept a block
    public Sprite gainSprite;
    // sprite to emote to when just lost or failed to get a block
    public Sprite lossSprite;
    // sprite for when potentially about to get a new block
    public Sprite hopeSprite;
    // sprite for when potentially about to lose a block
    public Sprite whineSprite;
    // sprite for when mouse hovers on tower
    public Sprite curiousSprite;

    // stores the default emote: happy, neutral, sad
    private Sprite defaultEmote;
    // returns true if an emoting coroutine is currently running
    private bool isEmoting;

    // component references
    private Image image;
    private EventBus eventBus;

    void Start () {
        this.image = this.GetComponent<Image> ();

        // bind events to different emotions
        this.eventBus = this.towerBindedTo.GetComponent<EventBus> ();
        this.eventBus.TopBlockGainEvent += HandleGain;
        this.eventBus.TopBlockLossEvent += HandleLoss;
        this.eventBus.EnterWhileDraggingEvent += HandleHope;
        this.eventBus.ExitWhileDraggingEvent += HandleWhine;
        this.eventBus.EnterNoDragEvent += HandleCurious;
        this.eventBus.ExitNoDragEvent += HandleDefault;
        this.eventBus.TopBlockKeepEvent += HandleKeep;
        this.eventBus.TopBlockMissEvent += HandleLoss;

        // initialize to sadSprite because everyone starts out with zero blocks
        this.image.sprite = defaultEmote = sadSprite;
    }

    // refresh default emote based on current tower height
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

    // handle gain of block event
    void HandleGain (Transform tower) {
        this.isEmoting = false;
        StopAllCoroutines ();
        StartCoroutine (this.EmoteToGain ());
        this.RefreshDefaultEmote ();
    }
    // coroutine for showing gainSprite for 1 second
    IEnumerator EmoteToGain () {
        this.isEmoting = true;
        this.image.sprite = gainSprite;
        yield return new WaitForSeconds (1f);
        this.image.sprite = defaultEmote;
        this.isEmoting = false;
    }

    // handle loss of block event
    void HandleLoss (Transform tower) {
        this.isEmoting = false;
        StopAllCoroutines ();
        StartCoroutine (this.EmoteToLoss ());
        this.RefreshDefaultEmote ();
    }
    // coroutine for showing lossSprite for 1 second
    IEnumerator EmoteToLoss () {
        this.isEmoting = true;
        this.image.sprite = lossSprite;
        yield return new WaitForSeconds (1f);
        this.image.sprite = defaultEmote;
        this.isEmoting = false;
    }

    // handle switch to hopeSprite
    void HandleHope (Transform tower) {
        this.isEmoting = false;
        StopAllCoroutines ();        
        this.image.sprite = hopeSprite;
    }

    // handle switch to whineSprite
    void HandleWhine (Transform tower) {
        this.isEmoting = false;
        StopAllCoroutines ();
        if (MouseActionTrigger.towerBeingDragged == tower) {
            this.image.sprite = whineSprite;
        } else {
            this.image.sprite = defaultEmote;
        }
    }

    // handle switch to curiousSprite
    void HandleCurious (Transform tower) {
        this.isEmoting = false;
        StopAllCoroutines ();
        this.image.sprite = curiousSprite;
    }

    // handle switch to defaultSprite
    void HandleDefault (Transform fromTower) {
        if (!this.isEmoting) {
            this.image.sprite = defaultEmote;
        }        
    }

    // handle emotion for keeping a block - will be the same as gain
    void HandleKeep(Transform tower) {
        this.HandleGain(tower);
    }
}