using UnityEngine;
using UnityEngine.UI;

public class HighResolutionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();

        button.onClick.AddListener(UpdateScreenResolution);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateScreenResolution()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
