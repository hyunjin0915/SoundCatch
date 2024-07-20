using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainGame
{
    hiddenSound, // 숨은 소리 찾기
    memorize, // 소리원 찾기
    tuningSoundNew1, // 음 맞추기 1단계
    tuningSoundNew2, // 음 맞추기 1단계
    tuningSoundNew3 // 음 맞추기 1단계
}
public class SceneLoader : Singleton<SceneLoader>
{
    public MainGame mainGame;
    public string curSceneName = "LoadingScene";
    [SerializeField]
    float transitionTime = 1f;

    public void ChangeScene(string _sceneName)
    {
        StartCoroutine(LoadSceneAsync(_sceneName));
    }
    IEnumerator LoadSceneAsync(string _sceneName)
    {
        if(curSceneName.Equals("SelectGame"))
        {
            SetMainGameName(_sceneName);
        }
        if(!curSceneName.Equals("HandTracking"))
        {
            SceneManager.UnloadSceneAsync(curSceneName);
        }
        AsyncOperation asyncLoad = 
            SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            if(asyncLoad.progress>=0.9f)
            {
                yield return new WaitForSeconds(transitionTime);
                asyncLoad.allowSceneActivation = true; 
            }
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
        curSceneName = _sceneName;
    }
    public void SetMainGameName(string _sceneName)
    {
        if(_sceneName.Equals("s"))
            {mainGame = MainGame.hiddenSound;
            Debug.Log("숨은소리찾기게임실행중");}
        else if (_sceneName.Equals("TuningSoundNew1"))
            mainGame = MainGame.tuningSoundNew1;
        else if (_sceneName.Equals("TuningSoundNew2"))
            mainGame = MainGame.tuningSoundNew2;
        else if (_sceneName.Equals("TuningSoundNew3"))
            mainGame = MainGame.tuningSoundNew3;
        else if(_sceneName.Equals("MemorizeLevel1"))
            mainGame = MainGame.memorize;
    }
}
