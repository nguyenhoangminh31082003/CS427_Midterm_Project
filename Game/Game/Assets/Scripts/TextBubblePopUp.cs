using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubblePopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
    }
}
