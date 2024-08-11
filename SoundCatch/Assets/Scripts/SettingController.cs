using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public void ClickButton0() // 시각적 표시 토글
    {
        Debug.Log("시각적 표시:토글");
        if (UIVisualController.Instance.visual == true)
        {
            Debug.Log("시각적 표시:끄기");
            UIVisualController.Instance.visual = false;
            UIVisualController.Instance.visualChange();
        } else if (UIVisualController.Instance.visual == false)
        {
            Debug.Log("시각적 표시:켜기");
            UIVisualController.Instance.visual = true;
            UIVisualController.Instance.visualChange();
        }
    }

    public void ClickButton1() // 게임선택 메뉴로
    {
        SceneLoader.Instance.ChangeScene("SelectGame");
    }
    public void ClickButton2() // 게임종료
    {
        SceneLoader.Instance.ChangeScene("GameExit");
    }
}