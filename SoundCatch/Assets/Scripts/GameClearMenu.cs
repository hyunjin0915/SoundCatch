using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearMenu : MonoBehaviour
{
    public void ClickButton0() //다시하기 선택
    {
        switch (SceneLoader.Instance.mainGame)
        {
            case MainGame.hiddenSound1:
                SceneLoader.Instance.ChangeScene("HiddenSound1");
                break;
            case MainGame.hiddenSound2:
                SceneLoader.Instance.ChangeScene("HiddenSound2");
                break;
            case MainGame.hiddenSound3:
                SceneLoader.Instance.ChangeScene("HiddenSound3");
                break;
            case MainGame.tuningSoundNew1:
                SceneLoader.Instance.ChangeScene("TuningSoundNew1");
                break;
            case MainGame.tuningSoundNew2:
                SceneLoader.Instance.ChangeScene("TuningSoundNew2");
                break;
            case MainGame.tuningSoundNew3:
                SceneLoader.Instance.ChangeScene("TuningSoundNew3");
                break;
            case MainGame.memorizeLevel1:
                SceneLoader.Instance.ChangeScene("MemorizeLevel1");
                break;
            case MainGame.memorizeLevel2:
                SceneLoader.Instance.ChangeScene("MemorizeLevel2");
                break;
            case MainGame.memorizeLevel3:
                SceneLoader.Instance.ChangeScene("MemorizeLevel3");
                break;

        }
    }

    public void ClickButton1()//게임선택 선택
    {
        SceneLoader.Instance.ChangeScene("SelectGame");
    }
    public void ClickButton2()//메인메뉴로 선택
    {
        SceneLoader.Instance.ChangeScene("MainScene");
    }
}
