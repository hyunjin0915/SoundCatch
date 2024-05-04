using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainGame
{
    hiddenSound, // 숨은 소리 찾기
    setSound, // 음 맞추기
    causeSound // 소리원 찾기
}
public class SceneLoader : Singleton<SceneLoader>
{
    public MainGame mainGame;
    string curSceneName = "HandTracking";
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
        if(_sceneName.Equals("HiddenSound"))
            {mainGame = MainGame.hiddenSound;
            Debug.Log("숨은소리찾기게임실행중");}
        else if(_sceneName.Equals("TuningSound"))
            mainGame = MainGame.setSound;
        else if(_sceneName.Equals("SoundSource"))
            mainGame = MainGame.causeSound;
    }
}
