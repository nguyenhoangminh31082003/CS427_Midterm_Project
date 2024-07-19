using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // This method will be called when the quit button is clicked
    public void Quit()
    {
        // If running in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a build
        Application.Quit();
#endif
    }
}
