using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionRadius = 2.0f;  
    private bool isPlayerInRange = false;
    private MainCharacter player;
    private Transform textBubble;
    protected DialogueManager dialogueManager;
    protected GameManager gameManager;

    protected virtual void Awake() {
        this.textBubble = this.transform.Find("TextBubble");
    }
    protected virtual void Start()
    {
        this.dialogueManager = DialogueManager.Instance;
        this.player = MainCharacter.Instance;
        this.gameManager = GameManager.Instance;
    }
    protected virtual void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= interactionRadius)
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

        if (!this.dialogueManager.isDialogueActive && this.player.IsButtonEDown() && this.isPlayerInRange)
        {
            this.Interact();
        }
    }

    protected abstract void Interact();

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
