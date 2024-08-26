using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public UIFunctionEvent uiFunEvent;
    public UIFunctionEvent uiOutlineEvent;
    public AudioSource guidevoiceSC;

    public GameObject gamepause;

    [SerializeField] private int uiNum = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // setting 창 열었을 때의 처리
            if (SceneManager.GetActiveScene().name == "Setting") {
                Debug.Log("오른쪽 화살표");
                if (uiNum < 3) 
                {
                    uiNum = 2;
                }
                if(uiNum != 2)
                {
                    uiOutlineEvent?.Raise(uiNum + 6);
                }
                else
                {
                    uiOutlineEvent?.Raise(9);
                }
                uiNum += 1;
                if (uiNum > 5)
                {
                    uiNum = 3;
                }
                uiOutlineEvent?.Raise(uiNum + 3);
            }
            else {
                if(uiNum > 2)      // setting에 사용한 후 원상복귀
                {
                    uiNum = -1;
                }
                if (uiNum != -1) // uiNum이 -1이 아닐 경우(현재 다른 UI에 아웃라인이 있는 경우)
                {
                    uiOutlineEvent?.Raise(uiNum + 3); // 현재 아웃라인이 있는 UI의 아웃라인을 끈다.
                }
                else // uiNum이 -1(InputManager가 처음 실행되었을 때)일 경우
                {
                    uiOutlineEvent?.Raise(3); // 첫 번째 UI의 OffOutline 실행(Outline을 끈다.)
                }
                uiNum += 1; // 다음 UI를 가리킨다.
                if (uiNum > 2) // 다음 UI가 2보다 클 경우(uiNum은 0 ~ 2까지만(3개) 있으므로).
                {
                    uiNum = 0; // uiNum을 0으로 첫번째 UI를 가리키도록 함
                }
                uiOutlineEvent?.Raise(uiNum); // 해당 UI의 아웃라인을 켠다.
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (SceneManager.GetActiveScene().name == "Setting")
            {
                Debug.Log("왼쪽 화살표");
                if (uiNum < 3)
                {
                    uiNum = 2;
                }
                if (uiNum != 2)
                {
                    uiOutlineEvent?.Raise(uiNum + 6);
                }
                else
                {
                    uiOutlineEvent?.Raise(11);
                }
                uiNum -= 1;
                if (uiNum < 3)
                {
                    uiNum = 5;
                }
                uiOutlineEvent?.Raise(uiNum + 3);
            }
            else
            {
                if (uiNum > 2)     // setting에 사용한 후 원상복귀
                {
                    uiNum = -1;
                }
                if (uiNum != -1)// uiNum이 -1이 아닐 경우(현재 다른 UI에 아웃라인이 있는 경우)
                {
                    uiOutlineEvent?.Raise(uiNum + 3); // 현재 아웃라인이 있는 UI의 아웃라인을 끈다.
                }
                else // uiNum이 -1(InputManager가 처음 실행되었을 때)일 경우
                {
                    uiOutlineEvent?.Raise(5); // 마지막 번째 UI의 OffOutline 실행(Outline을 끈다.)
                }
                uiNum -= 1;// 이전 UI를 가리킨다.
                if (uiNum < 0) // 이전 UI가 0보다 작을 경우(uiNum은 0 ~ 2까지만(3개) 있으므로).
                {
                    uiNum = 2;// uiNum을 2로 마지막 UI를 가리키도록 함
                }
                uiOutlineEvent?.Raise(uiNum); // 해당 UI의 아웃라인을 켠다.
            }            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!guidevoiceSC.isPlaying) // 안내음성이 출력되고 있지 않을 경우 
            {
                if(SceneManager.GetActiveScene().name == "Setting")
                {
                    if(uiNum == 4)
                    {
                        gamepause.GetComponent<GamePause>().Resume();
                        gamepause.GetComponent<GamePause>().paused = false;
                    }
                    uiFunEvent?.Raise(uiNum - 3);
                }
                else
                {
                    uiFunEvent?.Raise(uiNum); // 해당 UI의 함수 실행
                }
            }
        }
    }



}
