using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetComponent : MonoBehaviour
{
    private void Awake()
    {
        
    }
    void Start()
    {
        MainCharacter.Instance.SetDefaultValuesToPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
