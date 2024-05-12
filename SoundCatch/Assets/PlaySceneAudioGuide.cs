using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneAudioGuide : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioCueEventSO currentSceneGuideSO;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneGuideSO.RaisePlayAudio();
    }
    
    public void PlayGuideSound()
    {
        audioSource.clip = currentSceneGuideSO.audioClip;
        //isNowGuidePlaying = true;
        audioSource?.Play();
        //isNowGuidePlaying = false;
    }

    private void OnEnable()
    {
        currentSceneGuideSO.OnAudioCue += PlayGuideSound;
    }
    private void OnDisable()
    {
        currentSceneGuideSO.OnAudioCue -= PlayGuideSound;
    }

}
