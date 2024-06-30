using UnityEngine;

public class Flag : Interactable
{
    public string storyPlot;

    protected override void Interact()
    {
        Debug.Log("Interacting with the flag");
        DisplayStoryPlot();
    }

    private void DisplayStoryPlot()
    {
        Debug.Log(storyPlot);
    }

    // protected override void OnPlayerEnterRange()
    // {
    //     base.OnPlayerEnterRange();
        
    //     // Debug.Log("Player entered flag interaction range");
    // }

    // protected override void OnPlayerExitRange()
    // {
    //     base.OnPlayerExitRange();
        
    //     // Debug.Log("Player exited flag interaction range");
    // }
}
