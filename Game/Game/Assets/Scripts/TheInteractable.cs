using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TheInteractable : MonoBehaviour
{
    public float interactionRadius = 2.0f;
    private bool isPlayerInRange = false;

    private TheFirstPlayer theFirstPlayer;
    
    private Transform textBubble;
    protected DialogueManager dialogueManager;
    protected TheGameManager theGameManager;

    protected virtual void Awake() {
        textBubble = this.transform.Find("TextBubble");
    }
    protected virtual void Start()
    {
        dialogueManager = DialogueManager.Instance;
        theFirstPlayer = TheFirstPlayer.Instance;
        theGameManager = TheGameManager.Instance;
    }

    protected virtual void Update()
    {
        // Check the distance between the player and the interactable object
        float distance = Vector2.Distance(transform.position, theFirstPlayer.transform.position);

        if (distance <= interactionRadius)
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

        // Check if the player presses the "E" key
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            Debug.Log("press e");
            Interact();
        }
    }

    // Method called when the player presses "E" within range
    protected abstract void Interact();

    protected virtual void OnPlayerEnterRange()
    {
        if (textBubble) {
            textBubble.gameObject.SetActive(true);
        }
        
    }

    protected virtual void OnPlayerExitRange()
    {
        if (textBubble) {
            textBubble.gameObject.SetActive(false);
        }
        
    }

    // Draw the interaction radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
