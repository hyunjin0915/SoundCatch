using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuideManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioEventChannelSO MainAudioEventChannel;
    public AudioEventChannelSO SelectGameAudioEventChannel;
    public AudioEventChannelSO HiddenSoundAudioEventChannel;
    public AudioEventChannelSO SoundDicAudioEventChannel;
    public AudioEventChannelSO SoundSourceAudioEventChannel;
    public AudioEventChannelSO TuningSoundAudioEventChannel;
    public AudioEventChannelSO WaitLoadingAudioEventChannel;

    public void PlayGuideSound(AudioInfoSO _audioInfoSO)
    {
        audioSource.clip = _audioInfoSO.audioClip;
        //isNowGuidePlaying = true;
        audioSource?.Play();
        //isNowGuidePlaying = false;
    }

    private void OnEnable()
    {
        MainAudioEventChannel.OnAudioCue += PlayGuideSound;
        SelectGameAudioEventChannel.OnAudioCue += PlayGuideSound;
        HiddenSoundAudioEventChannel.OnAudioCue += PlayGuideSound;
        SoundDicAudioEventChannel.OnAudioCue += PlayGuideSound;
        SoundSourceAudioEventChannel.OnAudioCue += PlayGuideSound;
        TuningSoundAudioEventChannel.OnAudioCue += PlayGuideSound;
        WaitLoadingAudioEventChannel.OnAudioCue += PlayGuideSound;
    }
    private void OnDisable()
    {
        MainAudioEventChannel.OnAudioCue -= PlayGuideSound;
        SelectGameAudioEventChannel.OnAudioCue -= PlayGuideSound;
        HiddenSoundAudioEventChannel.OnAudioCue -= PlayGuideSound;
        SoundDicAudioEventChannel.OnAudioCue -= PlayGuideSound;
        SoundSourceAudioEventChannel.OnAudioCue -= PlayGuideSound;
        TuningSoundAudioEventChannel.OnAudioCue -= PlayGuideSound;
        WaitLoadingAudioEventChannel.OnAudioCue -= PlayGuideSound;
    }

}
