using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceShowed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Choice;
    public bool showed = false;

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive && !showed) {
            showed = true;
        }
        else if (!DialogueManager.Instance.isDialogueActive &&  showed)
        {
            showed = false;
            Choice.GetComponent<Canvas>().enabled = true;
        }
    }
}
