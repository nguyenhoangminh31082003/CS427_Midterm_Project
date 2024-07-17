using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{
    protected override void Interact()
    {
        // Call function to change scene
        Debug.Log("Change scene");
    }
}
