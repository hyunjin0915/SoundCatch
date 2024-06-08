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

        //if(!audioSource.isPlaying)
        //{
        //    StartCoroutine(waitAseconds());
        //}

        if(SceneLoader.Instance.curSceneName.Equals("HiddenSound"))
        {
            StartCoroutine(WaitHiddenSound());
        }
        else if(SceneLoader.Instance.curSceneName.Equals("TuningSound"))
        {
            StartCoroutine(WaitTuningSound());
        }
        else if(SceneLoader.Instance.curSceneName.Equals("SoundSource"))
        {
            StartCoroutine(WaitSSSound());
        }

    }

    private void OnEnable()
    {
        SceneInfoAGChannel.OnAudioCue += PlayGuideSound;

    }
    private void OnDisable()
    {
        SceneInfoAGChannel.OnAudioCue -= PlayGuideSound;
    }
    IEnumerator WaitHiddenSound()
    {
        yield return new WaitForSeconds(11f);
    }
    IEnumerator WaitTuningSound()
    {
        yield return new WaitForSeconds(8f);
    }
    IEnumerator WaitSSSound()
    {
        yield return new WaitForSeconds(8f);
    }
}
