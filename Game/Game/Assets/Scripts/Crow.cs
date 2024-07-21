using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    // Start is called before the first frame update
    private bool readyToDie = false;
    void Start()
    {
        
    }

    public void Appear()
    {
        GameObject vfx = GameObject.Find("DestroyVfx");
        Destructible destructible = vfx.GetComponent<Destructible>();
        destructible.Destruct();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.Instance.isDialogueActive && !readyToDie)
        {
            readyToDie = true;
        }
        else if (!DialogueManager.Instance.isDialogueActive && readyToDie)
        {
            GetComponent<Animator>().SetBool("Death", true);
            GameObject.Find("bone2").SetActive(false);
            readyToDie = false;
        }
    }
}
