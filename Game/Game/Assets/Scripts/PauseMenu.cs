using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public const double NUMBER_OF_MILLISECONDS_OF_TIMEOUT = 1000;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject endGameMenu;
    private bool isOn = false;
    private double lastTime = 0;

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
            if ((Time.time - this.lastTime) * 1000 <= NUMBER_OF_MILLISECONDS_OF_TIMEOUT)
                return;

            this.lastTime = Time.time;

            if (!this.isOn) 
                this.Pause();
            else 
                this.Resume();
        }
    }
}
