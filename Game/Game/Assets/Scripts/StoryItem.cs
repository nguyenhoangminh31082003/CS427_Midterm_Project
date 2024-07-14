using UnityEngine;

public class StoryItem : Interactable
{
    //public string storyPlot;

    [SerializeField] private Dialogue dialogue;
    public string backgroundTrack = "";
    protected override void Interact()
    {
        if (backgroundTrack.Length > 0)
        {
            AudioManager.Instance.currentTrack = backgroundTrack; 
            AudioManager.Instance.PlayMusic(backgroundTrack);
        }
        dialogueManager.StartDialogue(dialogue);
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }

}
