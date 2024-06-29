using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;

    public bool isChangingScene = false;
    private bool isTrigger = false;
    private bool isPlayerInRange = false;

    public void TriggerDialogue()
    {
        manager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
    }

    private void Update()
    {
        if (!isPlayerInRange) { return; }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isTrigger) { isTrigger = true; TriggerDialogue(); }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
        isTrigger = false;
    }

    private void FixedUpdate()
    {
        //if (isTrigger && !manager.isDialogueActive && isChangingScene)
        //{
        //    SceneController.Instance.NextLevel();
        //}
    }
}
