using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public bool paused = false;
    bool check = false;

    string before;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GameObject.Find("GuidevoiceManager").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if((SceneManager.GetActiveScene().name != "HandTracking") && (SceneManager.GetActiveScene().name != "Explanation"))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                check = true;
            }
            if (check)
            {
                if (paused)
                {
                    Pause();
                    check = false;
                }
                else if (!paused)
                {
                    Resume();
                    check = false;
                }
            }
        }
    }

    public void Pause()     // 게임 일시정지
    {
        before = SceneManager.GetActiveScene().name;
        if(audioSource.isPlaying) { audioSource.Pause(); }
        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }

    public void Resume()    // 게임 이어하기
    {
        Time.timeScale = 1;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(before));
        SceneManager.UnloadSceneAsync("Setting");
        audioSource.Play();
    }
}

