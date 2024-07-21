using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleDelay : MonoBehaviour
{
    public string demonName = "";
    private bool readyToExplode;

    private GameObject crow;
    void Start()
    {
        readyToExplode = false;
        crow = GameObject.Find(demonName);
        crow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToExplode && DialogueManager.Instance.isDialogueActive)
        {
            readyToExplode = true;
        }

        if (readyToExplode && !DialogueManager.Instance.isDialogueActive)
        {
            crow.SetActive(true);
            crow.GetComponent<Crow>().Appear();
            gameObject.GetComponent<MusicChanger>().ChangeMusic();
            gameObject.GetComponent<Destructible>().Destruct();
        }
    }
}
