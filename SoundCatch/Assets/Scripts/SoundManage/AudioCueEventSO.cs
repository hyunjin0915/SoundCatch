using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AudioCue Event")]
public class AudioCueEventSO : ScriptableObject
{
    public AudioClip audioClip;
    public event Action OnAudioCue;

    public void RaisePlayAudio()
    {
        OnAudioCue?.Invoke();
    }

    
}
