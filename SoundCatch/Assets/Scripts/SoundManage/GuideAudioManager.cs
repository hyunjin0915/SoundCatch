using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioCueEventSO audioCueEventSO;

    public bool isNowGuidePlaying = false;
    
    public void PlayGuideSound()
    {
        audioSource.clip = audioCueEventSO.audioClip;
        isNowGuidePlaying = true;
        audioSource?.Play();
        isNowGuidePlaying = false;
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
