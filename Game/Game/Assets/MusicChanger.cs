using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class MusicChanger : MonoBehaviour
{
    public string newTrack = "";
    // Start is called before the first frame update
    public void ChangeMusic() { 
        AudioManager.Instance.ChangeDefaultTrack(newTrack);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
