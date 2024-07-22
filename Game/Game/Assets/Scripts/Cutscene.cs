using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Animator transistionAnim;
    private bool transition = false;
    [SerializeField] private bool transitionAtEnd = true;
    void Start()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transistionAnim.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (!transition && DialogueManager.Instance.isDialogueActive)
        {
            transition = true;
        }
        else if (transition && !DialogueManager.Instance.isDialogueActive) {
            if (transitionAtEnd)
                NextLevel();
            transition = false;
        }
    }
}
