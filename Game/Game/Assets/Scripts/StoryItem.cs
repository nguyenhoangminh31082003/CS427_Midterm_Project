using UnityEngine;

public class StoryItem : Interactable
{
    //public string storyPlot;

    [SerializeField] private Dialogue dialogue;
    public int backgroundTrackIndex = -1;
    protected override void Interact()
    {
        if (backgroundTrackIndex > 0)
        {
            AudioManager.Instance.currentTrack = backgroundTrackIndex; 
            AudioManager.Instance.PlayMusic("theme_" + backgroundTrackIndex.ToString());
        }
        dialogueManager.StartDialogue(dialogue);
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }

}
