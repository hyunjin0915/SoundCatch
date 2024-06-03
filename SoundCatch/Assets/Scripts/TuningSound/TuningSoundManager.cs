using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TuningSoundManager : MonoBehaviour
{
    int stagePitch;
    int pPitch;

    int gesture;
    Vector3 handPos;
    Vector3 handPosOld;
    bool pitchChange;

    public AudioSource audioSource;
    public AudioSource subAudioSource;
    public AudioClip clip;
    public AudioClip[] sounds;
    public AudioInfoSO _ClickRightBlock;
    public AudioEventChannelSO _ClickRightBlockEC;

    GameObject ht;
    float time = 0.0f;      // 판정 타이머용
    float asTime = 0.0f;    // 스테이지 음 반복재생용

    private GameObjectEventListener listener;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        ht = GameObject.FindGameObjectWithTag("HTManager");
        audioSource = ht.GetComponent<AudioSource>();
        subAudioSource = ht.GetComponentInChildren<AudioSource>();
        listener = GetComponent<GameObjectEventListener>();

        // 시작할 때 0~6 사이 범위 안에서 랜덤하게 스테이지 피치를 설정
        stagePitch = UnityEngine.Random.Range(0, 6);
        UnityEngine.Debug.Log("stagePitch : " + stagePitch);
        asTime = 0.0f;
        audioSource.Stop();
        audioSource.PlayOneShot(sounds[stagePitch]);

        audioSource.clip = sounds[stagePitch];
        audioSource.Play();

        // 시작 플레이어 피치를 0('도'음)으로 지정
        pPitch = 0;
        subAudioSource.clip = sounds[pPitch];

        // 손 위치 정보 가져오기
        handPos = ht.GetComponent<HandTracking>().getViewportPoint();
        handPosOld = handPos;

        UnityEngine.Debug.Log("Start. handPos : " + handPos);
    }

    void Update()
    {
        // 스테이지 음 반복 재생
        asTime += Time.deltaTime;
        if (asTime >= 5.0f)
        {
            audioSource.PlayOneShot(sounds[stagePitch]);
            asTime = 0.0f;
        }

        // 손 위치 정보 갱신
        handPosOld = handPos;
        handPos = ht.GetComponent<HandTracking>().getViewportPoint();
        // UnityEngine.Debug.Log("Update. handPos : " + handPos);

        // 손 위치가 변경되면 이벤트 발생
        if (handPosOld != handPos)
        {
            listener.OnTSEventRaised();
            // UnityEngine.Debug.Log("Event. handPos : " + handPos);
        }

        // 손 모양이 주먹이면 카운트
        if(gesture == 1)
        {
            time += Time.deltaTime;
        }
    }

    public void checkAnswer()
    {
        // viewport point의 y값에 따라 피치 변경
        int pPitchOld = pPitch;
        setpPitch();

        UnityEngine.Debug.Log("pPitch : " + pPitch);

        if (pPitch != pPitchOld)    // 변경 시 소리 출력
        {
            subAudioSource.Stop();
            subAudioSource.clip = sounds[pPitch];
            subAudioSource.Play();
            pitchChange = true;
        }


        // 주먹을 쥐었는지 여부 가져오기. 쥐었으면 1, 폈으면 0
        gesture = ht.GetComponent<HandTracking>().getGestureInfo();
        UnityEngine.Debug.Log("Gesture: " + gesture);

        if ((gesture == 1) && (time >= 0.75f) && (pitchChange))
        {
            // 피치를 맞췄다면
            if (pPitch == stagePitch)
            {
                // 게임 승리
                if (subAudioSource.isPlaying)
                {
                    subAudioSource.Stop();
                }
                subAudioSource.clip = sounds[7];
                subAudioSource.Play();
                
                // 게임 클리어 씬으로 전환
                _ClickRightBlockEC.RaisePlayAudio(_ClickRightBlock);
                SceneLoader.Instance.ChangeScene("GameClear");
            }
            else
            {
                // 틀렸음 표시하기
                if (subAudioSource.isPlaying)
                {
                    subAudioSource.Stop();
                }
                subAudioSource.clip = sounds[8];
                subAudioSource.Play();
                UnityEngine.Debug.Log("Try Again");

            }

            // 타이머 초기화
            time = 0.0f;
            // 다음에 위치가 변경될때까지 판정 안 함
            pitchChange = false;
        }
    }

    // 손 위치에 따른 pPitch 판정부. 길어서 따로 뺌
    void setpPitch()
    {
        if (handPos[1] < 0.25)
        {
            pPitch = 0;
        }
        else if (handPos[1] < 0.35)
        {
            pPitch = 1;
        }
        else if (handPos[1] < 0.45)
        {
            pPitch = 2;
        }
        else if (handPos[1] < 0.55)
        {
            pPitch = 3;
        }
        else if (handPos[1] < 0.65)
        {
            pPitch = 4;
        }
        else if (handPos[1] < 0.75)
        {
            pPitch = 5;
        }
        else
        {
            pPitch = 6;
        }
    }
}
