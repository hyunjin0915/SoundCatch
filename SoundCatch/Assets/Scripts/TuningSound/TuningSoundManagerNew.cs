using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuningSoundManagerNew : MonoBehaviour
{
    int stagePitch;

    public AudioSource audioSource;
    public AudioSource subAudioSource;
    public AudioClip[] sounds;

    public AudioInfoSO _ClickRightBlock;
    public AudioEventChannelSO _ClickRightBlockEC;

    float asTime = 1.0f;    // 스테이지 음 반복재생용

    GameObject ht;
    // private GameObjectEventListener listener;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        // listener = GetComponent<GameObjectEventListener>();

        ht = GameObject.FindGameObjectWithTag("HTManager");
        audioSource = ht.GetComponent<AudioSource>();
        subAudioSource = ht.GetComponentInChildren<AudioSource>();

        // 시작할 때 0~6 사이 범위 안에서 랜덤하게 스테이지 피치를 설정
        stagePitch = UnityEngine.Random.Range(0, 6);
        asTime = -3.5f;
        audioSource.clip = sounds[stagePitch];
        // 시작 플레이어 피치를 0('도'음)으로 지정
        // pPitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 스테이지 음 반복 재생
        asTime += Time.deltaTime;
        if (asTime >= 5.0f)
        {
            audioSource.PlayOneShot(sounds[stagePitch]);
            asTime = 0.0f;
        }
    }

    public void checkAnswer(int obNum)
    {
        Debug.Log("Check");
        if(obNum == stagePitch)
        {
            Debug.Log("정답");
            // 게임 클리어 씬으로 전환
            audioSource.loop = false;
            _ClickRightBlockEC.RaisePlayAudio(_ClickRightBlock);
            SceneLoader.Instance.ChangeScene("GameClear");
        }
        else
        {
            Debug.Log("오답");
        }
    }
}
