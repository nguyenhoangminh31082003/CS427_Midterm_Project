using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxCanvasUI : MonoBehaviour
{

    private GameObject boxImage;
    private GameObject weaponImage;
    private GameObject firstCounterBackground;
    private GameObject secondCounterBackground;
    private GameObject firstCounterText;
    private GameObject secondCounterText;


    // Start is called before the first frame update
    void Start()
    {

        List<GameObject> list = new List<GameObject>();

        foreach(Transform child in this.transform)
        {
            list.Add(child.gameObject);
            Debug.Log(child.name);
        }

        Debug.Log("-------------------");

        this.boxImage = list[0];
        this.weaponImage = list[1];
        this.firstCounterBackground = list[2];
        this.firstCounterText = list[3];
        this.secondCounterBackground = list[4];
        this.secondCounterText = list[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
