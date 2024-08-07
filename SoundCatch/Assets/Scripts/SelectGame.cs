using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    public void ClickButton0() //숨은 소리 찾기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("hiddenSound");
    }

    public void ClickButton1()//음 맞추기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("tuningSound");
    }
    public void ClickButton2()//소리원 찾기
    {
        SceneLoader.Instance.ChangeScene("SelectLevel");
        SceneLoader.Instance.SetMainGameName("memorize");
    }
}
