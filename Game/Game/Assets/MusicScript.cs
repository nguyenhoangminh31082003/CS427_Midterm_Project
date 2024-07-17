using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var slider = GetComponentInChildren<Slider>();
        var toggle = GetComponentInChildren<Toggle>();

        toggle.onValueChanged.AddListener(MuteMusic);
        slider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Slider>().interactable = !GetComponentInChildren<Toggle>().isOn;
    }

    void MuteMusic(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : 0.5f;
    }

    public void SetVolume(float volume)
    {
        if (GetComponentInChildren<Toggle>().isOn)
        {
            AudioListener.volume = volume;
        }
    }
}
