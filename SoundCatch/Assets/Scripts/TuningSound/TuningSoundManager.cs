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

    public AudioSource audioSource;
    public AudioSource subAudioSource;
    public AudioClip clip;
    public AudioClip[] sounds;

    GameObject ht;
    float time = 0.0f;
    float asTime = 0.0f;

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

    private void FixedUpdate()
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
            UnityEngine.Debug.Log("Event. handPos : " + handPos);
        }

        // 손 모양이 주먹이면 카운트
        time += Time.deltaTime;
    }

    public void checkAnswer()
    {
        // viewport point의 y값에 따라 피치 변경
        int pPitchOld = pPitch;
        if (handPos[1] < 0.55)
        {
            pPitch = 0;
        }
        else if (handPos[1] < 0.65)
        {
            pPitch = 1;
        }
        else if (handPos[1] < 0.75)
        {
            pPitch = 2;
        }
        else if (handPos[1] < 0.85)
        {
            pPitch = 3;
        }
        else if (handPos[1] < 0.95)
        {
            pPitch = 4;
        }
        else if (handPos[1] < 1.05)
        {
            pPitch = 5;
        }
        else
        {
            pPitch = 6;
        }

        UnityEngine.Debug.Log("pPitch : " + pPitch);

        if (pPitch != pPitchOld)    // 변경 시 소리 출력
        {
            subAudioSource.Stop();
            subAudioSource.clip = sounds[pPitch];
            subAudioSource.Play();
        }


        // 주먹을 쥐었는지 여부 가져오기. 쥐었으면 1, 폈으면 0
        gesture = ht.GetComponent<HandTracking>().getGestureInfo();
        UnityEngine.Debug.Log("Gesture: " + gesture);

        if ((gesture == 1) && (time >= 2.0f))
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
                // 게임 일시정지
                // Time.timeScale = 0.0f;
                // UnityEngine.Debug.Log("Win");
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
        }
    }
}
