using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioCueEventSO audioCueEventSO;
    
    public void PlayGuideSound()
    {
        audioSource.clip = audioCueEventSO.audioClip;
        audioSource?.Play();
    }

    private void OnEnable()
    {
        audioCueEventSO.OnAudioCue += PlayGuideSound;
    }
    private void OnDisable()
    {
        audioCueEventSO.OnAudioCue -= PlayGuideSound;
    }
}
