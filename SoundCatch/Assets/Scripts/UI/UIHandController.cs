using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 손 위치를 따라다니며 손을 쥐었는지 폈는지 알려주는 ui 구현을 위한 스크립트
public class UIHandController : MonoBehaviour
{
    // 스프라이트 등록
    public Sprite handClose;
    public Sprite handOpen;
    public Sprite handV;

    // 이미지
    Image handImg;

    // 손 움직임을 따라가기 위함
    GameObject ht;
    RectTransform rectTransform;
    Vector3 handPos;
    int gesture;

    // Start is called before the first frame update
    void Start()
    {
        handImg = GetComponent<Image>();
        ht = GameObject.FindGameObjectWithTag("HTManager");
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 손 위치에 맞게 손 UI 이동
        handPos = ht.GetComponent<HandTracking>().getHandPos();
        rectTransform.position = handPos;

        // 손 모양에 맞게 손 UI 모양 변경
        gesture = ht.GetComponent<HandTracking>().getGestureInfo();
        if (gesture == 0 )
        {
            handImg.sprite = handOpen;
        } else if (gesture == 1 )
        {
            handImg.sprite = handClose;
        }
        else if(gesture == 2 )
        {
            handImg.sprite = handV;
        }
    }
}
