using UnityEngine;

public class TheStoryItem : TheInteractable
{

    [SerializeField] private Dialogue dialogue;
    public string backgroundTrack = "";
    protected override void Interact(int whichPlayer)
    {
        if (backgroundTrack.Length > 0)
        {
            AudioManager.Instance.PlayMusic(backgroundTrack);
        }
        dialogueManager.StartDialogue(dialogue);
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }

}
