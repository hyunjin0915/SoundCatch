using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearMenu : MonoBehaviour
{
    public void ClickButton0() //다시하기 선택
    {
        if((SceneLoader.Instance.mainGame == MainGame.hiddenSound1) || (SceneLoader.Instance.mainGame == MainGame.hiddenSound2) || (SceneLoader.Instance.mainGame == MainGame.hiddenSound3))
        {
            SceneLoader.Instance.ChangeScene("HiddenSound1");
        }
        else if(SceneLoader.Instance.mainGame == MainGame.tuningSoundNew1)
        {
            SceneLoader.Instance.ChangeScene("TuningSoundNew1");
        }
        else
        {
            SceneLoader.Instance.ChangeScene("Memorize");
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
