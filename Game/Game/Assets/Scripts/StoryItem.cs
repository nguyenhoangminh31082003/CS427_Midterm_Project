using UnityEngine;

public class StoryItem : Interactable
{
    //public string storyPlot;

    [SerializeField] private Dialogue dialogue;
    protected override void Interact()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }

}
