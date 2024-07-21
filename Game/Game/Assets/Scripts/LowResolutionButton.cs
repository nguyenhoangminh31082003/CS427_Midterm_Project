using UnityEngine;

public class LowResolutionButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<UnityEngine.UI.Button>();

        button.onClick.AddListener(UpdateScreenResolution);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateScreenResolution()
    {
        Screen.SetResolution(800, 600, true);
    }
}
