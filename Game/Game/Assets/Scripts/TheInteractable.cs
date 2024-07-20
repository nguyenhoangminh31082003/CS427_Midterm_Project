using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public abstract class TheInteractable : MonoBehaviour
{
    public float interactionRadius = 2.0f;
    private bool isPlayerInRange = false;

    private TheFirstPlayer theFirstPlayer;
    private TheSecondPlayer theSecondPlayer;
    
    private Transform textBubble;
    protected DialogueManager dialogueManager;
    protected TheGameManager theGameManager;

    protected virtual void Awake() {
        this.textBubble = this.transform.Find("TextBubble");
    }
    protected virtual void Start()
    {
        dialogueManager = DialogueManager.Instance;
        this.theFirstPlayer = TheFirstPlayer.Instance;
        this.theSecondPlayer = TheSecondPlayer.Instance;
        this.theGameManager = TheGameManager.Instance;
    }

    protected virtual void Update()
    {
        float   firstDistance   =   Vector2.Distance(transform.position, theFirstPlayer.transform.position),
                secondDistance  =   Vector2.Distance(transform.position, theSecondPlayer.transform.position);

        if (firstDistance < secondDistance)
        {
            if (firstDistance <= interactionRadius)
            {
                if (!isPlayerInRange)
                {
                    isPlayerInRange = true;
                    OnPlayerEnterRange();
                }

            }
            else
            {
                if (isPlayerInRange)
                {
                    isPlayerInRange = false;
                    OnPlayerExitRange();
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
            {
                Debug.Log("press e");
                Interact(1);
            }
        }
        else if (secondDistance < firstDistance)
        {
            if (secondDistance <= interactionRadius)
            {
                if (!this.isPlayerInRange)
                {
                    this.isPlayerInRange = true;
                    this.OnPlayerEnterRange();
                }

            }
            else
            {
                if (this.isPlayerInRange)
                {
                    this.isPlayerInRange = false;
                    this.OnPlayerExitRange();
                }
            }

            if (Input.GetKeyDown(KeyCode.O) && isPlayerInRange)
            {
                Debug.Log("press o");
                Interact(2);
            }
        }

        
    }

    // Method called when the player presses "E" (with the first player) or "O" (with the second player) within range
    protected abstract void Interact(int whichPlayer);

    protected virtual void OnPlayerEnterRange()
    {
        if (this.textBubble) {
            this.textBubble.gameObject.SetActive(true);
        }
        
    }

    protected virtual void OnPlayerExitRange()
    {
        if (this.textBubble) {
            this.textBubble.gameObject.SetActive(false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
