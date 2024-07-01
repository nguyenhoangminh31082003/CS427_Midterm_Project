using UnityEngine;

public class Flag : Interactable
{
    public string storyPlot;

    [SerializeField] private Dialogue dialogue;

    protected override void Interact()
    {
        Debug.Log("Interacting with the flag");
        DialogueManager.Instance.StartDialogue(dialogue);
    }

}
