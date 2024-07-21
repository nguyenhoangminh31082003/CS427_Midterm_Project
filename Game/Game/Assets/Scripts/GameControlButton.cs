using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlButton : MonoBehaviour
{

    const int NUMBER_OF_FRAME_FOR_A_CLICK = 5;

    private int lastClickedFame;
    private Button button;

    void Start()
    {
        this.button = this.gameObject.GetComponent<Button>();

        this.lastClickedFame = 0;

        this.button.onClick.AddListener(() =>
        {
            this.lastClickedFame = Time.frameCount;
        });
        
        //Debug.Log(this.gameObject + " " + (this.button != null));
    }

    void Update()
    {
        
    }

    public bool IsClicked()
    {
        return (Time.frameCount - this.lastClickedFame) <= NUMBER_OF_FRAME_FOR_A_CLICK;
    }
}
