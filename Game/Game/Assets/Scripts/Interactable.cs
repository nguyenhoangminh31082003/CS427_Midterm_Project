using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionRadius = 2.0f;  // Radius within which the player can interact
    private bool isPlayerInRange = false;
    private MainCharacter player;
    private Transform textBubble;

    protected virtual void Awake() {
        textBubble = this.transform.Find("TextBubble");
    }
    protected virtual void Start()
    {
        player = MainCharacter.Instance;
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Check the distance between the player and the interactable object
        float distance = Vector2.Distance(transform.position, player.transform.position);

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
        textBubble.gameObject.SetActive(true);
    }

    protected virtual void OnPlayerExitRange()
    {
        textBubble.gameObject.SetActive(false);
    }

    // Draw the interaction radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
