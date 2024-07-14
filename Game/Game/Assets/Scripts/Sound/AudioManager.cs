using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private String musicName;
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public int startTrack = 5;
    public int currentTrack = -1;

    public void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        PlayMusic(musicName);
    }
    public void PlayMusic(string name) {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("Music sound not found");
        }
        else {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name) {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null) {
            Debug.Log("SFX sound not found");
        }
        else {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void Update()
    {
        if (!musicSource.isPlaying)
        {
            if (currentTrack != -1)
            {
                PlayMusic("theme_" + currentTrack.ToString());
            }
            else
            {
                PlayMusic("theme_" + startTrack.ToString());
            }
        }
    }
    public void ToggleMusic() {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX() {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float v) {
        musicSource.volume = v;
    }

    public void SFXVolume(float v) {
        sfxSource.volume = v;
    }
}
