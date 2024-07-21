using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickHandler : MonoBehaviour
{
    private DialogueManager dialogueManager;
    void Start()
    {
        this.dialogueManager = DialogueManager.Instance;
    }

    void Update()
    {
        if (dialogueManager != null)
        {
            if (dialogueManager.isDialogueActive)
            {
                this.gameObject.SetActive(false);
                return;
            }
        }
        this.gameObject.SetActive(true);
    }
}
