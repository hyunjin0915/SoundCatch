using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainGame
{
    hiddenSound,
    hiddenSound1, // 숨은 소리 찾기
    hiddenSound2, // 숨은 소리 찾기
    hiddenSound3, // 숨은 소리 찾기
    memorize,
    memorizeLevel1, // 소리원 찾기
    memorizeLevel2, // 소리원 찾기
    memorizeLevel3, // 소리원 찾기
    tuningSound,
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
        if (curSceneName.Equals("SelectLevel"))
        {
            SetMainGameName(_sceneName);
        }
        if (!curSceneName.Equals("HandTracking"))
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
        if(_sceneName.Equals("hiddenSound1"))
            {mainGame = MainGame.hiddenSound1;}
        else if(_sceneName.Equals("hiddenSound2"))
            {mainGame = MainGame.hiddenSound2;}
        else if(_sceneName.Equals("hiddenSound3"))
            {mainGame = MainGame.hiddenSound3;}
        else if (_sceneName.Equals("TuningSoundNew1"))
            mainGame = MainGame.tuningSoundNew1;
        else if (_sceneName.Equals("TuningSoundNew2"))
            mainGame = MainGame.tuningSoundNew2;
        else if (_sceneName.Equals("TuningSoundNew3"))
            mainGame = MainGame.tuningSoundNew3;
        else if(_sceneName.Equals("MemorizeLevel1"))
            mainGame = MainGame.memorizeLevel1;
        else if (_sceneName.Equals("MemorizeLevel2"))
            mainGame = MainGame.memorizeLevel2;
        else if (_sceneName.Equals("MemorizeLevel3"))
            mainGame = MainGame.memorizeLevel3;
        else if (_sceneName.Equals("hiddenSound"))
            mainGame = MainGame.hiddenSound;
        else if (_sceneName.Equals("tuningSound"))
            mainGame = MainGame.tuningSound;
        else if (_sceneName.Equals("memorize"))
            mainGame = MainGame.memorize;
    }
}
