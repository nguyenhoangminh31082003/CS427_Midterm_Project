using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected int number;
    [SerializeField] protected bool currentlyUsed;

    // Start is called before the first frame update
    void Start()
    {
        this.number = 0;

        this.currentlyUsed = false;

        this.gameObject.SetActive(this.currentlyUsed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
