using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crow : MonoBehaviour
{
    // Start is called before the first frame update
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
        
    }
}
