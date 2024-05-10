using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneAudioGuide : MonoBehaviour
{
    public AudioCueEventSO currentSceneGuideSO;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneGuideSO.RaisePlayAudio();
    }
    
    
}
