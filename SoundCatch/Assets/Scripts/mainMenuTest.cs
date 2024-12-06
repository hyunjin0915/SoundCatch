using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuTest : MonoBehaviour
{
    private UDPReceive udp;
    
    private void Start()
    {
        udp = GameObject.FindGameObjectWithTag("UDPReceive").GetComponent<UDPReceive>(); ;
    }

    public void ClickButton0() //게임설정
    {
        SceneLoader.Instance.ChangeScene("Setting");
    }

    public void ClickButton1()//게임시작선택
    {
        SceneLoader.Instance.ChangeScene("SelectGame");
    }
    public void ClickButton2()//게임시작선택
    {
        udp.ExitHandTracking();
        Application.Quit();
    }
}
