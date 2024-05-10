using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSoundManager : MonoBehaviour
{
    public void ClickRightBlock() //정답 블록
    {
        Debug.Log("정답입니다");
    }
     public void WrongBlock()
    {
        Debug.Log("오답입니다");
    }
     public void ClickBackGround()
    {
        Debug.Log("배경입니다");
    }
   
}
