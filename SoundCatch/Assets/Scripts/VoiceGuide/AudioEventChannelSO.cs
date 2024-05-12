using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AudioEventChannel")]
public class AudioEventChannelSO : ScriptableObject
{
    public event Action<AudioInfoSO> OnAudioCue;

    public void RaisePlayAudio(AudioInfoSO _audioInfoSO)
    {
        Debug.Log("가이드음성재생");
        OnAudioCue?.Invoke(_audioInfoSO);
    }
}
