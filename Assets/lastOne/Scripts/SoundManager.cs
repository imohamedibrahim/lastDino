using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    internal enum AUDIO_LIST
    {
        BUTTON_AUDIO,
        METEO_HIT_AUTIO,
        MAIN_THEME_AUDIO
    }
    [SerializeField]
    private AudioSource mainThemeAudioSource;
    [SerializeField]
    private AudioClip mainThemeAudioClip;
    [SerializeField]
    private AudioClip meteoHitAudioClip;
    [SerializeField]
    private AudioClip buttonAudioClip;
    private AudioSource audioSource;
    private static SoundManager soundManager;
    public static SoundManager Instance()
    {
        if (soundManager == null)
        {
            soundManager = new SoundManager();
        }
        return soundManager;
    }

    public void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            soundManager = null;
        }
        else
        {
            soundManager = this;
        }
        audioSource = GetComponent<AudioSource>();
    }



    internal void PlayAudio(AUDIO_LIST audioToPlay)
    {
        if(audioToPlay == AUDIO_LIST.BUTTON_AUDIO)
        {
            audioSource.PlayOneShot(buttonAudioClip);
        }
        else if (audioToPlay == AUDIO_LIST.MAIN_THEME_AUDIO)
        {
            mainThemeAudioSource.Play();
        }
        else if(audioToPlay == AUDIO_LIST.METEO_HIT_AUTIO)
        {
            audioSource.PlayOneShot(meteoHitAudioClip);
        }
    }

    internal void SetMuteState(bool tmp)
    {
        mainThemeAudioSource.mute = tmp;
        audioSource.mute = tmp;
    }

    internal void StopAudio(AUDIO_LIST audioToStop)
    {
        if(audioToStop == AUDIO_LIST.MAIN_THEME_AUDIO)
        {
            mainThemeAudioSource.Stop();
        }
    }
}
