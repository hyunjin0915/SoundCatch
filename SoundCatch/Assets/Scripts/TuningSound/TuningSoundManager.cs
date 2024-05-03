using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TuningSoundManager : MonoBehaviour
{
    int stagePitch;
    int pPitch;
    bool gesture;
    Vector3 handPos;
    HandTracking ht = new HandTracking();
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // 시작할 때 1~8 사이 범위 안에서 랜덤하게 스테이지 피치를 설정
        stagePitch = UnityEngine.Random.Range(1, 9);
        // 시작 플레이어 피치를 1('도'음)으로 지정
        pPitch = 1;

    }

    // Update is called once per frame
    void Update()
    {
        // 손 정보 받아오기
        handPos = ht.getHandInfo();
        // 손 좌표를 viewport point로 변환
        handPos = cam.WorldToViewportPoint(handPos);
        // viewport point의 y값에 따라 피치 변경
        pPitch = (int)System.Math.Truncate((double)(handPos[1] * 10 / 8));

        // 주먹을 쥐었는지 여부 가져오기. 쥐었으면 true, 폈으면 false
        gesture = ht.getGestureInfo();

        // 주먹을 쥐었을 때
        if (gesture)
        {
            // 피치를 맞췄다면
            if (pPitch == stagePitch)
            {
                // 게임 승리
                Debug.Log("Win");
            }
        }
    }
}
