using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explanation : MonoBehaviour
{
    private HandTracking hT;

    public AudioInfoSO _curSceneAudioGuide;
    public AudioInfoSO paper_AudioGuide;
    public AudioInfoSO rock_AudioGuide;
    public AudioInfoSO v_AudioGuide;
    public AudioEventChannelSO _curSceneAudioEventChannel;

    public void MoveMain()
    {
        SceneLoader.Instance.ChangeScene("MainScene");
    }

    void Start()
    {
        GameObject htManager = GameObject.FindGameObjectWithTag("HTManager");
        hT = htManager.GetComponent<HandTracking>();
        StartCoroutine(PlayAudioGuides());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _curSceneAudioEventChannel.RaisePlayAudio(_curSceneAudioGuide);
            StartCoroutine(PlayAudioGuides());
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneLoader.Instance.ChangeScene("MainScene");
            GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().enabled = true;
        }
    }

    IEnumerator PlayAudioGuides()
    {
        yield return new WaitForSeconds(_curSceneAudioGuide.audioClip.length + 3.0f);
        yield return new WaitUntil(CheckGesture_Paper);
        /*
        if(hT.getGestureInfo() == 0)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(paper_AudioGuide);
        }*/
        yield return new WaitForSeconds(paper_AudioGuide.audioClip.length + 5.0f);
        yield return new WaitUntil(CheckGesture_Rock);
        /*if (hT.getGestureInfo() == 1)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(rock_AudioGuide);
        }*/
        yield return new WaitForSeconds(rock_AudioGuide.audioClip.length + 5.0f);
        /*if(hT.getGestureInfo() == 2)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(v_AudioGuide);
        }*/
        yield return new WaitUntil(CheckGesture_V);
    }

    public bool CheckGesture_Paper()
    {
        if (hT.getGestureInfo() == 0)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(paper_AudioGuide);
            return true;
        } else
        {
            return false;
        }
    }

    public bool CheckGesture_Rock()
    {
        if (hT.getGestureInfo() == 1)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(rock_AudioGuide);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckGesture_V()
    {
        if (hT.getGestureInfo() == 2)
        {
            _curSceneAudioEventChannel.RaisePlayAudio(v_AudioGuide);
            return true;
        }
        else
        {
            return false;
        }
    }
}
