using UnityEngine;
using UnityEngine.EventSystems;

public class GameControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    const int NUMBER_OF_FRAME_FOR_A_CLICK = 1;

    private DialogueManager dialogueManager;
    private int lastDownFrame;
    private int lastUpFrame;

    void Start()
    {
        this.lastDownFrame = 0;

        this.lastUpFrame = 0;

        this.dialogueManager = DialogueManager.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.lastDownFrame = Time.frameCount;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.lastUpFrame = Time.frameCount;
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

    public bool IsDown()
    {
        return (Time.frameCount - this.lastDownFrame) <= NUMBER_OF_FRAME_FOR_A_CLICK;
    }

    public bool IsUp()
    {
        return (Time.frameCount - this.lastUpFrame) <= NUMBER_OF_FRAME_FOR_A_CLICK;
    }
}
