using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    public void Pause() {
        
        mainMenu.SetActive(true);
    }

    public void Resume() {
        mainMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
    }
}
