using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Slider _musicSlider, _sfxSlider;
    [SerializeField] Toggle _musicToggle, _sfxToggle;
    
    void Start() {
        // on: true -> tick -> tat nhac -> mute = true
        _musicSlider.value = AudioManager.Instance.musicSource.volume;
        _musicToggle.isOn = AudioManager.Instance.musicSource.mute;

        _sfxSlider.value = AudioManager.Instance.sfxSource.volume;
        _sfxToggle.isOn = AudioManager.Instance.sfxSource.mute;
    }
    public void ToggleMusic() {
        Debug.Log("ToggleMusic");

        if (_musicToggle.isOn)
            _musicSlider.interactable = false;
        else
            _musicSlider.interactable = true;

        AudioManager.Instance.ToggleMusic(_musicToggle.isOn);
        AudioManager.Instance.SaveDataToPlayerPrefs();
    }

    public void ToggleSFX() {
        Debug.Log("ToggleSFX");

        if (_sfxToggle.isOn)
            _sfxSlider.interactable = false;
        else
            _sfxSlider.interactable = true;

        AudioManager.Instance.ToggleSFX(_sfxToggle.isOn);
        AudioManager.Instance.SaveDataToPlayerPrefs();
    }

    public void MusicVolume() {
        Debug.Log("MusicVolume");
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        AudioManager.Instance.SaveDataToPlayerPrefs();
    }

    public void SFXVolume() {
        Debug.Log("_sfxSlider.value: "+_sfxSlider.value);
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
        AudioManager.Instance.SaveDataToPlayerPrefs();
    }
}
