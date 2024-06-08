using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGuideManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioEventChannelSO SceneInfoAGChannel;

    public void PlayGuideSound(AudioInfoSO _audioInfoSO)
    {
        audioSource.clip = _audioInfoSO.audioClip;
        audioSource?.Play();

    }

    private void OnEnable()
    {
        SceneInfoAGChannel.OnAudioCue += PlayGuideSound;

    }
    private void OnDisable()
    {
        SceneInfoAGChannel.OnAudioCue -= PlayGuideSound;
    }
}
