using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    string curSceneName = "HandTracking";
    [SerializeField]
    float transitionTime = 1f;

    public void ChangeScene(string _sceneName)
    {
        StartCoroutine(LoadSceneAsync(_sceneName));
    }
    IEnumerator LoadSceneAsync(string _sceneName)
    {
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
}
