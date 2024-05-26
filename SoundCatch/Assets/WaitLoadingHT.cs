using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLoadingHT : MonoBehaviour
{
    public AudioInfoSO _curSceneAudioGuide;
    public AudioEventChannelSO _curSceneAudioEventChannel;
    // Start is called before the first frame update
    void Start()
    {
        _curSceneAudioEventChannel.RaisePlayAudio(_curSceneAudioGuide);
    }
}
