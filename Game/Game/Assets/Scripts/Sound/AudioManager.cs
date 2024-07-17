using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private String musicName = "";
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public string currentTrack = "";

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        LoadDataFromPlayerPrefs();
        PlayMusic(musicName);
    }
    public void PlayMusic(string name)
    {
        if (name == null) {
            PlayMusic(musicName);
            return;
        }
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void Update()
    {
        if (!musicSource.isPlaying)
        {
            if (currentTrack.Length == 0)
            {
                PlayMusic(musicName);
            }
            else
            {
                PlayMusic(currentTrack);
            }
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float v)
    {
        musicSource.volume = v;
    }

    public void SFXVolume(float v)
    {
        sfxSource.volume = v;
    }

    public void ChangeDefaultTrack(string newTrack)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX sound not found");
        }
        else
        {
            this.currentTrack = newTrack; 
            this.PlayMusic(newTrack);
        }
    }

    public void LoadDataFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("sound_music_state"))
            musicSource.mute = bool.Parse(PlayerPrefs.GetString("sound_music_state"));
        if (PlayerPrefs.HasKey("sound_sfx_state"))
            sfxSource.mute = bool.Parse(PlayerPrefs.GetString("sound_sfx_state"));
        if (PlayerPrefs.HasKey("sound_music_volumn"))
            musicSource.volume = PlayerPrefs.GetFloat("sound_music_volumn");
        if (PlayerPrefs.HasKey("sound_sfx_volumn"))
            sfxSource.volume = PlayerPrefs.GetFloat("sound_sfx_volumn");
    }

    public void SaveDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("sound_music_state", musicSource.mute.ToString());
        PlayerPrefs.SetString("sound_sfx_state", sfxSource.mute.ToString());
        PlayerPrefs.SetFloat("sound_music_volumn", musicSource.volume);
        PlayerPrefs.SetFloat("sound_sfx_volumn", sfxSource.volume);

        Debug.Log(musicSource.mute.ToString());
        Debug.Log(sfxSource.mute.ToString());
        Debug.Log(musicSource.volume);
        Debug.Log(sfxSource.volume);
    }

    // public void SetDefaultValuesToPlayerPrefs()
    // {
    //     PlayerPrefs.SetString("sound_music_state", true.ToString());
    //     PlayerPrefs.SetString("sound_sfx_state", true.ToString());
    //     PlayerPrefs.SetFloat("sound_music_volumn", 1);
    //     PlayerPrefs.SetFloat("sound_sfx_volumn", 1);
    // }
}
