using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    private HandTracking hT;

    public AudioEventChannelSO _curSceneAudioEventChannel;
    public AudioInfoSO _curSceneAudioGuide;

    public AudioInfoSO hiddenSound_AudioGuide;

    public AudioInfoSO tuningSound_AudioGuide;

    public AudioInfoSO memorize_AudioGuide;

    void Start()
    {
        GameObject htManager = GameObject.FindGameObjectWithTag("HTManager");
        hT = htManager.GetComponent<HandTracking>();
    }

    public void ClickButton0() //숨은 소리 찾기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("hiddenSound");
    }

    public void ClickButton1()//음 맞추기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("tuningSound");
    }
    public void ClickButton2()//소리원 찾기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("memorize");
    }

    public void ExplainBtn1()
    {
        _curSceneAudioEventChannel.RaisePlayAudio(hiddenSound_AudioGuide);
    }
    public void ExplainBtn2()
    {
        _curSceneAudioEventChannel.RaisePlayAudio(tuningSound_AudioGuide);
    }
    public void ExplainBtn3()
    {
        _curSceneAudioEventChannel.RaisePlayAudio(memorize_AudioGuide);
    }
}
