using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject endGameMenu;
    private bool isOn = false;

    public void Pause() {
        this.isOn = true;
        this.mainMenu.SetActive(true);
    }

    public void Resume() {
        this.isOn = false;
        this.mainMenu.SetActive(false);
    }

    void Update() {

        if (MainCharacter.Instance.IsDead())
        {
            if (!this.isOn)
            {
                this.endGameMenu.SetActive(true);
                this.isOn = true;
            }
            return;
        }

        if (MainCharacter.Instance.IsButtonPDown()) 
        {
            Debug.Log(Time.time);
            if (!this.isOn) 
                this.Pause();
            else 
                this.Resume();
        }
    }
}
