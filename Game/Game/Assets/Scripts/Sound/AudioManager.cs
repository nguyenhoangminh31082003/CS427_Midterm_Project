using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

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
        PlayMusic("theme_5");
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
}
