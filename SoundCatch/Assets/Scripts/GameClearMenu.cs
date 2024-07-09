using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearMenu : MonoBehaviour
{
    public void ClickButton0() //다시하기 선택
    {
        if(SceneLoader.Instance.mainGame == MainGame.hiddenSound)
        {
            SceneLoader.Instance.ChangeScene("HiddenSound");
        }
        else if(SceneLoader.Instance.mainGame == MainGame.setSound)
        {
            SceneLoader.Instance.ChangeScene("TuningSound");
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
