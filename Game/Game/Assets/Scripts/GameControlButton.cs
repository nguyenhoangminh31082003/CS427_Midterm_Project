using UnityEngine;
using UnityEngine.EventSystems;

public class GameControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] bool inactiveByDialogue = false;

    public const int NUMBER_OF_FRAMES_OF_TIMEOUT = 1;

    private DialogueManager dialogueManager;
    private int lastDownFrame;
    private int lastUpFrame;
    
    void Start()
    {
        this.lastDownFrame = Time.frameCount - NUMBER_OF_FRAMES_OF_TIMEOUT;

        this.lastUpFrame = Time.frameCount - NUMBER_OF_FRAMES_OF_TIMEOUT;

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
        return (Time.frameCount - this.lastDownFrame) <= NUMBER_OF_FRAMES_OF_TIMEOUT;
    }

    public bool IsUp()
    {
        return (Time.frameCount - this.lastUpFrame) <= NUMBER_OF_FRAMES_OF_TIMEOUT;
    }
}
