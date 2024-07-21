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
        if (this.dialogueManager != null)
        {
            if (this.dialogueManager.isDialogueActive)
            {
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                return;
            }
        }

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
