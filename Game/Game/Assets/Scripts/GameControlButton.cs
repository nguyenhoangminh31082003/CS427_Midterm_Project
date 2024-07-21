using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlButton : MonoBehaviour
{

    const int NUMBER_OF_FRAME_FOR_A_CLICK = 5;

    private DialogueManager dialogueManager;
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

        this.dialogueManager = DialogueManager.Instance;
    }

    void Update()
    {
        if (this.dialogueManager != null)
        {
            if (this.dialogueManager.isDialogueActive)
            {
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                return;
            }
        }

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public bool IsClicked()
    {
        return (Time.frameCount - this.lastClickedFame) <= NUMBER_OF_FRAME_FOR_A_CLICK;
    }
}
