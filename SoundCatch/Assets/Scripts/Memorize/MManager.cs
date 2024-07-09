using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memorize : MonoBehaviour
{
    private HandTracking hT;
    private AudioSource audioSource;

    public AudioClip[] clips; // 0 : open 1: close 2 : scissor
    public AudioClip[] resultClips; // 0 : 정답 1 : 실패
    public AudioClip testStart; // 기억하세요 같은 문구
    public AudioClip answerStart; // 시작!
    public AudioClip completeClip; // 클리어
    public AudioClip bingoClip; // 정답 
    public AudioClip failClip; // 실패
    public AudioClip[] countdownClip;

    private int[] level_1 = new int[3];
    private int[] level_2 = new int[5];
    private int[] level_3 = new int[7];
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject htManager = GameObject.FindGameObjectWithTag("HTManager");
        hT = htManager.GetComponent<HandTracking>();
        audioSource = htManager.GetComponent<AudioSource>();
        audioSource.panStereo = 0;
        audioSource.volume = 0.8f;
        audioSource.loop = false;

        for (int i = 0; i < 7; i++)
        {
            if (i < 3)
            {
                level_1[i] = Random.Range(0, 3);
                level_2[i] = Random.Range(0, 3);
                level_3[i] = Random.Range(0, 3);
            } else if (i < 5)
            {
                level_2[i] = Random.Range(0, 3);
                level_3[i] = Random.Range(0, 3);
            } else if (i < 7)
            {
                level_3[i] = Random.Range(0, 3);
            }
        }

        Invoke("StartGameCor", 8.5f);
    }

    public void StartGameCor()
    {
        StartCoroutine(PlayGame());
    }

    IEnumerator GameStart(int[] soundArr)
    {
        yield return StartCoroutine(PlayTest(soundArr));
        PlayAudio(answerStart);
        for (int i = 0; i < soundArr.Length; i++)
        {
            yield return StartCoroutine(Countdown()); // 카운트다운
            if (hT.getGestureInfo() == soundArr[i]) // 정답
            {
                PlayAudio(bingoClip);
                yield return new WaitForSeconds(1.0f);
            } else // 정답이 아닐 경우
            {
                PlayAudio(failClip);
                // 게임 오버로 게임 클리어 씬으로 이동
                yield return new WaitForSeconds(1.0f);
                SceneLoader.Instance.ChangeScene("GameClear");
            }
        }

        yield break;
    }


    IEnumerator PlayTest(int[] soundArr) // 외워야 할 사운드 재생
    {
        for (int i = 0; i < soundArr.Length; i++)
        {
            PlayAudio(clips[soundArr[i]]);
            yield return new WaitForSeconds(2.0f);
        }
        yield break;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1.0f); // 시작 사운드 사이즈 보고 판단
        PlayAudio(countdownClip[0]);
        yield return new WaitForSeconds(1.0f);
        PlayAudio(countdownClip[0]);
        yield return new WaitForSeconds(1.0f);
        PlayAudio(countdownClip[1]);
        yield return new WaitForSeconds(1.0f); 
    }

    IEnumerator PlayGame()
    {
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    yield return StartCoroutine(GameStart(level_1));
                    break;
                case 1:
                    yield return StartCoroutine(GameStart(level_2));
                    break;
                case 2:
                    yield return StartCoroutine(GameStart(level_3));
                    break;
                case 3:
                    SceneLoader.Instance.ChangeScene("GameClear");
                    break;
            }
        }

        // 게임 클리어 씬으로 이동.
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
