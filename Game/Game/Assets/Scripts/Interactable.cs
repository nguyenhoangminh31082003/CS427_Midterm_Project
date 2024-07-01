using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionRadius = 2.0f;  // Radius within which the player can interact
    private bool isPlayerInRange = false;
    private MainCharacter player;

    protected virtual void Awake() {
        player = MainCharacter.Instance;
    }
    protected virtual void Start()
    {
        
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

    protected virtual void OnPlayerEnterRange()
    {
        // vi du: canvas.gameObject.SetActive(true);
    }

    protected virtual void OnPlayerExitRange()
    {
        // vi du: canvas.gameObject.SetActive(false);
    }

    // Draw the interaction radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
