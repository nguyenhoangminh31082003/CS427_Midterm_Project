using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    private DialogueLine currentLine;
    private Queue<DialogueLine> lines;

    private int index = 0;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.05f;
    public Canvas canvas;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
        canvas = transform.parent.GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        AudioManager.Instance.PlaySFX("text_sfx");
        canvas.enabled = true;
        isDialogueActive = true;
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            AudioManager.Instance.currentTrack = -1;
            AudioManager.Instance.musicSource.Stop();
            return;
        }

        currentLine = lines.Dequeue();

        index = 0;
        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));

        AudioManager.Instance.PlaySFX("text_sfx");
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            if (index == dialogueLine.line.Length) break;
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            index++;
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        canvas.enabled = false;
    }

    public void Update()
    {
        if (isDialogueActive && index == currentLine.line.Length) {
            AudioManager.Instance.sfxSource.Stop();
        }
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (index < currentLine.line.Length)
            {
                StopAllCoroutines();
                index = currentLine.line.Length;
                dialogueArea.text = currentLine.line;
            }
            else
            {
                DisplayNextDialogueLine();
            }
        }
    }
}
