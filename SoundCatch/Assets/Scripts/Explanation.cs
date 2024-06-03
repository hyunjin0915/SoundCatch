using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    public AudioInfoSO _curSceneAudioGuide;
    public AudioEventChannelSO _curSceneAudioEventChannel;

    public void MoveMain()
    {
        SceneLoader.Instance.ChangeScene("MainScene");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _curSceneAudioEventChannel.RaisePlayAudio(_curSceneAudioGuide);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneLoader.Instance.ChangeScene("MainScene");
        }
    }
}
