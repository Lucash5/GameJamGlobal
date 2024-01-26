using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource soundEffectAS;
    public AudioSource musicAS;

    [Header("Settings")]
    public AudioMixer masterAudioMixer;
    public Slider masterSlider;
    public Slider soundEffectSlider;
    public Slider musicSlider;

    [Header("Music Tracks")]
    public Music mainMenuMusic;
    public Music gameMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySoundEffectOnce(SoundEffect soundEffect)
    {
        soundEffectAS.spatialBlend = 0;

        if (soundEffect.mixerGroup != null)
        {
            soundEffectAS.outputAudioMixerGroup = soundEffect.mixerGroup;
        }

        if (soundEffect.clips.Length == 0)
        {
            Debug.Log("Audioclips not added to sound effect!");
            return;
        }

        soundEffectAS.PlayOneShot(soundEffect.GetRandomClip(), soundEffect.volume);
    }

    public void PlaySoundEffectOnce(SoundEffect soundEffect, GameObject sourceObject)
    {
        if (soundEffect.clips.Length == 0)
        {
            Debug.Log("Audioclips not added to sound effect!");
            return;
        }

        AudioSource sourceToPlayFrom = sourceObject.GetComponent<AudioSource>();

        // Onko l‰hteest‰ josta halutaan ‰‰ni toistaa olemassa olevaa AudioSourcea
        if (sourceToPlayFrom == null)
        {
            // Lis‰t‰‰n AudioSource komponentti objektiin josta ‰‰ni halutaan toistaa
            sourceToPlayFrom = sourceObject.AddComponent<AudioSource>();
        }

        if (soundEffect.mixerGroup != null)
        {
            sourceToPlayFrom.outputAudioMixerGroup = soundEffect.mixerGroup;
        }

        sourceToPlayFrom.spatialBlend = soundEffect.spatialBlend;
        sourceToPlayFrom.PlayOneShot(soundEffect.GetRandomClip(), soundEffect.volume);
    }

    public void ChangeAudioMixerGroupVolume(string mixerGroup)
    {
        if (mixerGroup == "Master")
        {
            masterAudioMixer.SetFloat("Master", masterSlider.value);
        }
        else if (mixerGroup == "SoundEffect")
        {
            masterAudioMixer.SetFloat("SoundEffect", soundEffectSlider.value);
        }
        else if (mixerGroup == "Music")
        {
            masterAudioMixer.SetFloat("Music", musicSlider.value);
        }
    }

    public void PlayMusicTrack(Music music)
    {
        if (music.musicTracks.Length == 0)
            return;

        musicAS.volume = music.volume;
        musicAS.loop = music.loopMusic;
        musicAS.clip = music.GetRandomTrack();
        musicAS.Play();
    }

}

[System.Serializable]
public class Music
{
    [Range(0f, 1f)]
    public float volume;
    public bool loopMusic;

    public AudioClip[] musicTracks;

    public AudioClip GetRandomTrack()
    {
        int random = UnityEngine.Random.Range(0, musicTracks.Length);

        return musicTracks[random];
    }
}

[System.Serializable]
public class SoundEffect
{
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float spatialBlend;
    public AudioMixerGroup mixerGroup;
    public AudioClip[] clips;

    public AudioClip GetRandomClip()
    {
        int random = UnityEngine.Random.Range(0, clips.Length);

        return clips[random];
    }
}
