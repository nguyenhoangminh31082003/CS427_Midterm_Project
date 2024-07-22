using UnityEngine;
using UnityEngine.EventSystems;

public class GameControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public const double NUMBER_OF_MILLISECONDS_OF_TIMEOUT = 1000;

    [SerializeField] bool inactiveByDialogue = false;

    public const int NUMBER_OF_FRAME_OF_TIMEOUT = 1;

    private DialogueManager dialogueManager;
    private int lastDownFrame;
    private int lastUpFrame;
    private double lastTime;

    void Start()
    {
        this.lastDownFrame = 0;

        this.lastUpFrame = 0;

        this.lastTime = 0;

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
        if ((Time.time - this.lastTime) * 1000 <= NUMBER_OF_MILLISECONDS_OF_TIMEOUT)
            return false;

        if ((Time.frameCount - this.lastDownFrame) <= NUMBER_OF_FRAME_OF_TIMEOUT)
        {
            this.lastTime = Time.time;
            return true;
        }

        return false;
    }

    public bool IsUp()
    {
        return (Time.frameCount - this.lastUpFrame) <= NUMBER_OF_FRAME_OF_TIMEOUT;
    }
}
