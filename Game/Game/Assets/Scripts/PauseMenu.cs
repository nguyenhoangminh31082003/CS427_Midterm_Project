using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject endGameMenu;
    private bool isOn = false;

    public void Pause() {
        isOn = true;
        mainMenu.SetActive(true);
    }

    public void Resume() {
        isOn = false;   
        mainMenu.SetActive(false);
    }

    void Update() {

        if (MainCharacter.Instance.IsDead())
        {
            if (!isOn)
            {
                endGameMenu.SetActive(true);
                isOn = true;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            if (!isOn) Pause();
            else Resume();
        }
    }
}
