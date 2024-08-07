using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public void ClickButton0() // 레벨 1
    {
        switch (SceneLoader.Instance.mainGame)
        {
            case MainGame.hiddenSound:
                SceneLoader.Instance.ChangeScene("HiddenSound1");
                break;
            case MainGame.memorize:
                SceneLoader.Instance.ChangeScene("MemorizeLevel1");
                break;
            case MainGame.tuningSound:
                SceneLoader.Instance.ChangeScene("TuningSoundNew1");
                break;
        }
    }

    public void ClickButton1() // 레벨 2
    {
        switch (SceneLoader.Instance.mainGame)
        {
            case MainGame.hiddenSound:
                SceneLoader.Instance.ChangeScene("HiddenSound2");
                break;
            case MainGame.memorize:
                SceneLoader.Instance.ChangeScene("MemorizeLevel2");
                break;
            case MainGame.tuningSound:
                SceneLoader.Instance.ChangeScene("TuningSoundNew2");
                break;
        }
    }
    public void ClickButton2() // 레벨 3
    {
        switch (SceneLoader.Instance.mainGame)
        {
            case MainGame.hiddenSound:
                SceneLoader.Instance.ChangeScene("HiddenSound3");
                break;
            case MainGame.memorize:
                SceneLoader.Instance.ChangeScene("MemorizeLevel3");
                break;
            case MainGame.tuningSound:
                SceneLoader.Instance.ChangeScene("TuningSoundNew3");
                break;
        }
    }
}
