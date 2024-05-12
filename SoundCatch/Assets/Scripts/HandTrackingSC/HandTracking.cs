using System.Collections.Generic;
using UnityEngine;

enum HandGesture
{
    paper, // 손바닥
    rock, // 주먹
    scissors, // 가위
    nothing
}


public class HandTracking : MonoBehaviour
{
    // event
    public UIFunctionEvent uiFunEvent;
    public GameObjectFunctionEvent gameObjectFunEvent;

    private bool isCameraOn = false; // 파이썬 파일이 실행되었는지

    // 인식
    private RaycastHit hit;
    public GameObject preHit;
    private bool isHandRock = false; // 손 모양이 주먹인가
    private HandGesture handGesture;    // 손 모양. getGestureInfo()에 필요해 저장하도록 변경
    // 손 인식 중심 좌표
    private float x;
    private float y;
    private float z;
    // 인식 타이머
    private float rockTime = 0.0f;

    // 오디오
    public AudioSource audioSource;
    public AudioSource subAscr;
    private Sound sound;
    private Sound subsound;

    // 사운드 타이머
    private float soundTimer = 0.0f;

    // layer
    private int bLayer = 1 << 3; // 레이어 3 : Background(경계)
    private int uLayer = 1 << 5; // 레이어 5 : UI
    private int gLayer = 1 << 6; // 레이어 6 : GameObject

    void Start()
    {
        
    }

    void Update()
    {
        string data = UDPReceive.instance.data; // 인식 데이터 받기

        if (data != "")
        {
            if (!data.Equals("true")) // 인식 데이터가 좌표일 경우
            {
                // 인식 데이터 전처리
                data = data.Remove(0, 1);
                data = data.Remove(data.Length - 1, 1);
                string[] points = data.Split(',');

                // 인식 좌표의 중간 좌표 가져오기(레이 발사할 부분만 가져오기)
                x = 7 - float.Parse(points[27]) / 100;
                y = float.Parse(points[28]) / 100;
                z = float.Parse(points[29]) / 100;

                // 손 모양 가져오기
                float handG = float.Parse(points[63]);
                handGesture = (HandGesture)((int)handG);

                Vector3 handCenter = new Vector3(x, y, z);

                Debug.DrawRay(handCenter, Vector3.forward, Color.blue, 300.0f); // 임시 레이어 표시

                if (Physics.Raycast(handCenter, Vector3.forward, out hit, 300.0f, bLayer | uLayer | gLayer)) 
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("UI")) // 인식한 오브젝트가 UI인 경우
                    {
                        // 소리 출력
                        PlaySound(3.0f);
                        
                        // UI 3초 주먹 인식
                        if (CognizeHandGesture(handGesture, 3.0f))
                        {
                            // UI 오브젝트의 경우 해당 오브젝트가 선택되었을 때
                            uiFunEvent.Raise(sound.objectNum);
                        }
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Background")) // 인식한 오브젝트가 Background인 경우
                    {
                        // 소리 출력
                        PlayLoopSound();

                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("GameObject")) // 인식한 오브젝트가 GameObject인 경우
                    {
                        // 게임 구분
                        switch (SceneLoader.Instance.mainGame)
                        {
                            case MainGame.hiddenSound: // 숨은 소리 찾기의 게임 오브젝트 인식 부분

                                // 소리 출력
                                // 인식한 오브젝트가 소리를 계속 반복해서 출력하면 PlayLoopSound()
                                // 인식한 오브젝트가 소리를 몇 초간 간격을 가지고 반복해서 출력하면 PlaySound(간격 초)
                                PlayLoopSound();

                                // 게임 오브젝트 주먹 인식
                                if (CognizeHandGesture(handGesture, 3.0f)) // 매개변수 3.0f 수정해서 원하는 초 만큼 주먹을 쥐어야 함수 실행 가능
                                {
                                    // 게임 오브젝트의 경우 해당 오브젝트가 선택되었을 때
                                    uiFunEvent.Raise(sound.objectNum);
                                }

                                break;
                            case MainGame.setSound: // 음 맞추기의 게임 오브젝트 인식 부분

                                // 소리 출력
                                // 인식한 오브젝트가 소리를 계속 반복해서 출력하면 PlayLoopSound()
                                // 인식한 오브젝트가 소리를 몇 초간 간격을 가지고 반복해서 출력하면 PlaySound(간격 초)

                                // 게임 오브젝트 주먹 인식
                                if (CognizeHandGesture(handGesture, 3.0f)) // 매개변수 3.0f 수정해서 원하는 초 만큼 주먹을 쥐어야 함수 실행 가능
                                {
                                    // 게임 오브젝트의 경우 해당 오브젝트가 선택되었을 때
                                    gameObjectFunEvent.Raise(sound.objectNum);
                                }

                                break;
                            case MainGame.causeSound: // 소리원 찾기의 게임 오브젝트 인식 부분

                                // 소리 출력
                                PlayLoopSound();
                                gameObjectFunEvent.SSRaise(handCenter);

                                // 게임 오브젝트 주먹 인식
                                if (CognizeHandGesture(handGesture, 3.0f)) // 매개변수 3.0f 수정해서 원하는 초 만큼 주먹을 쥐어야 함수 실행 가능
                                {
                                    // 게임 오브젝트의 경우 해당 오브젝트가 선택되었을 때
                                    gameObjectFunEvent.Raise(sound.objectNum);
                                }

                                break;
                        }
                    }

                    preHit = hit.transform.gameObject;
                }
            }
            else // 인식 데이터가 true일 경우 => 파이썬 파일이 실행되었을 때의 타이밍을 위한 조건
            {
                isCameraOn = true;
            }
        }
    }

    // 손 모양 인식
    private bool CognizeHandGesture(HandGesture hand, float timer)
    {
        bool rockFinish = false;
        
        switch (hand)
        {
            case HandGesture.rock:
                isHandRock = true;

                // 초 인식
                rockTime += Time.deltaTime;

                if (rockTime >= timer)
                {
                    rockFinish = true;
                    rockTime = 0.0f;
                }
                break;
            default:
                isHandRock = false;
                rockTime = 0.0f;
                break;
        }

        return rockFinish;
    }
    
    // 소리 출력(GameObject, Background와 같이 계속 반복해서 들려주는 오디오)
    private void PlayLoopSound()
    {
        if (preHit == null || hit.collider.gameObject != preHit)
        {
            sound = hit.collider.GetComponent<Sound>();
            subAscr.Stop();
            if(sound.isSub)
            {
                subAscr.loop = true;
                subAscr.panStereo = -1;
                audioSource.panStereo = 1;
                subAscr.clip = sound.subSound;
                subAscr.volume = sound.volume;
                subAscr.Play();
            }
            else
            {
                audioSource.panStereo = 0;
                subAscr.panStereo = 0;
            }
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = sound.cubeSound;
            audioSource.volume = sound.volume;
            audioSource.Play();

            rockTime = 0.0f;
        } 
    }

    // 소리 출력(UI와 같이 간격을 주고 반복해서 들려주는 오디오)
    private void PlaySound(float timer) // 매개변수 : 들려주는 간격을 몇 초로 줄 것인지.
    {

        if (preHit == null || hit.collider.gameObject != preHit)
        {
            sound = hit.collider.GetComponent<Sound>();

            audioSource.Stop();
            audioSource.volume = sound.volume;
            audioSource.PlayOneShot(sound.cubeSound);
            audioSource.clip = sound.cubeSound;

            rockTime = 0.0f;
            soundTimer = 0.0f;
        } else
        {
            if (!audioSource.isPlaying)
            {
                soundTimer += Time.deltaTime;

                if (soundTimer >= timer)
                {
                    audioSource.PlayOneShot(sound.cubeSound);
                    soundTimer = 0.0f;
                }
            }
        }
    }

    // 현재 게임 설정
    /*public void SetCurScene(MainGame gameName)
    {
        mainGame = gameName;
    }
*/
    // 현재 손 위치 정보를 viewportPoint로 변환한 값 리턴
    public Vector3 getViewportPoint()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 pos = new Vector3(x, y, z);
        pos = cam.WorldToViewportPoint(pos);

        return pos;
    }

    // 현재 손 모양 리턴
    public int getGestureInfo()
    {
        int n = 0;
        if (handGesture == HandGesture.paper)
        {
            n = 0;
        }
        else if (handGesture == HandGesture.rock)
        {
            n = 1;
        }
        else if (handGesture == HandGesture.scissors)
        {
            n = 2;
        }


        return n;
    }

    // 현재 손 위치 정보 리턴
    public Vector3 getHandPos()
    {
        Vector3 pos = new Vector3(x, y, z);

        return pos;
    }
}
