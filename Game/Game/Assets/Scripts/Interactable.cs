using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionRadius = 2.0f;  // Radius within which the player can interact
    private bool isPlayerInRange = false;
    public GameObject player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        // player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player == null) return;

        // Check the distance between the player and the interactable object
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= interactionRadius)
        {
            isPlayerInRange = true;
            OnPlayerEnterRange();
            // Check if the player presses the "E" key
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
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
    }

    // Method called when the player presses "E" within range
    protected abstract void Interact();

    // Optional: Method called when the player enters the interaction range
    protected virtual void OnPlayerEnterRange()
    {
        // You can add visual feedback or other effects here
    }

    // Optional: Method called when the player exits the interaction range
    protected virtual void OnPlayerExitRange()
    {
        // You can add visual feedback or other effects here
    }

    // Optional: Draw the interaction radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
