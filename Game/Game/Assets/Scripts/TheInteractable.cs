using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using TMPro;

public abstract class TheInteractable : MonoBehaviour
{
    public float interactionRadius = 2.0f;
    private bool isPlayerInRange = false;

    private TheFirstPlayer theFirstPlayer;
    private TheSecondPlayer theSecondPlayer;

    private Transform textBubble;
    protected DialogueManager dialogueManager;
    protected TheGameManager theGameManager;

    private string content;

    protected virtual void Awake()
    {
        this.textBubble = this.transform.Find("TextBubble");
    }
    protected virtual void Start()
    {
        this.dialogueManager = DialogueManager.Instance;
        this.theFirstPlayer = TheFirstPlayer.Instance;
        this.theSecondPlayer = TheSecondPlayer.Instance;
        this.theGameManager = TheGameManager.Instance;
    }

    protected virtual void Update()
    {
        float firstDistance = Vector2.Distance(transform.position, theFirstPlayer.transform.position),
                secondDistance = Vector2.Distance(transform.position, theSecondPlayer.transform.position);
        bool isTheFirstPlayerAttacking = Input.GetKeyDown(KeyCode.E),
                isTheSecondPlayerAttacking = Input.GetKeyDown(KeyCode.O);

        if (firstDistance <= this.interactionRadius && secondDistance <= this.interactionRadius)
        {
            this.content = "E|O";

            this.isPlayerInRange = true;

            this.OnPlayerEnterRange();

            if (isTheFirstPlayerAttacking && isTheSecondPlayerAttacking)
            {

                if (firstDistance < secondDistance)
                    this.Interact(1);
                else if (secondDistance < firstDistance)
                    this.Interact(2);

                return;
            }

            if (isTheFirstPlayerAttacking)
            {
                this.Interact(1);
            }

            if (isTheSecondPlayerAttacking)
            {
                this.Interact(2);
            }

            return;
        }

        if (firstDistance <= this.interactionRadius)
        {
            this.content = "E";

            this.isPlayerInRange = true;

            this.OnPlayerEnterRange();

            if (isTheFirstPlayerAttacking)
            {
                this.Interact(1);
            }

            return;
        }

        if (secondDistance <= this.interactionRadius)
        {
            this.content = "O";

            this.isPlayerInRange = true;

            this.OnPlayerEnterRange();

            if (isTheSecondPlayerAttacking)
            {
                this.Interact(2);
            }

            return;
        }

        this.isPlayerInRange = false;

        this.OnPlayerExitRange();
    }

    // Method called when the player presses "E" (with the first player) or "O" (with the second player) within range
    protected abstract void Interact(int whichPlayer);

    protected virtual void OnPlayerEnterRange()
    {
        if (this.textBubble)
        {
            Transform text = this.textBubble.Find("Text");

            if (text != null)
            {
                TextMeshPro component = text.GetComponent<TextMeshPro>();
                if (component != null)
                {
                    component.text = this.content;
                }
            }


            this.textBubble.gameObject.SetActive(true);
        }

    }

    protected virtual void OnPlayerExitRange()
    {
        if (this.textBubble)
        {
            this.textBubble.gameObject.SetActive(false);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}