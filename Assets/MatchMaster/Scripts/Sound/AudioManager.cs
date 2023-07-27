using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using SNG.Save;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] backgroundMusicSounds;
    public Sound[] soundEffectSounds;

    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Create a single AudioSource for background music
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();

        foreach (Sound s in backgroundMusicSounds)
        {
            s.source = backgroundMusicSource;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }

        // Create a single AudioSource for sound effects
        soundEffectSource = gameObject.AddComponent<AudioSource>();

        foreach (Sound s in soundEffectSounds)
        {
            s.source = soundEffectSource;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }

        if (SaveGame.Instance.GeneralData.IsMusicOn)
        {
            PlayBackgroundMusic("bg_music");
        }
    }

    public void PlayBackgroundMusic(string sound)
    {
        Sound s = Array.Find(backgroundMusicSounds, item => item.name == sound);
        if (s != null)
        {
            backgroundMusicSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Background music not found: " + sound);
        }
    }

	public void StopBackgroundMusic(string sound)
	{
		Sound s = Array.Find(backgroundMusicSounds, item => item.name == sound);
		s.source.Stop();
	}

    public void PlaySoundEffect(string sound)
    {
        Sound s = Array.Find(soundEffectSounds, item => item.name == sound);
        if (s != null)
        {
            soundEffectSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("Sound effect not found: " + sound);
        }
    }

    public void StopSoundEffect(string sound)
    {
        Sound s = Array.Find(soundEffectSounds, item => item.name == sound);
        s.source.Stop();
    }
}