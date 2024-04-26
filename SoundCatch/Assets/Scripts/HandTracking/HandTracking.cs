using UnityEngine;

enum HandGesture
{
    paper, // 손바닥
    rock // 주먹
}

public enum MainGame
{
    hiddenSound, // 숨은 소리 찾기
    setSound, // 음 맞추기
    causeSound // 소리원 찾기
}

public class HandTracking : MonoBehaviour
{
    public MainGame mainGame = MainGame.causeSound;

    // event
    public UIFunctionEvent uiFunEvent;
    public GameObjectFunctionEvent gameObjectFunEvent;

    private bool isCameraOn = false; // 파이썬 파일이 실행되었는지

    // 인식
    private RaycastHit hit;
    private RaycastHit preHit;
    private bool isHandRock = false; // 손 모양이 주먹인가
    // 인식 타이머
    private float rockTime = 0.0f;

    // 오디오
    public AudioSource audioSource;
    public AudioSource subAscr;
    private Sound sound;
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
                float x = 7 - float.Parse(points[27]) / 100;
                float y = float.Parse(points[28]) / 100;
                float z = float.Parse(points[29]) / 100;

                // 손 모양 가져오기
                float handG = float.Parse(points[63]);
                HandGesture handGesture = (HandGesture)((int)handG);

                Vector3 handCenter = new Vector3(x, y, z);

                Debug.DrawRay(handCenter, Vector3.forward, Color.blue, 300.0f); // 임시 레이어 표시

                if (Physics.Raycast(handCenter, Vector3.forward, out hit, 300.0f, bLayer | uLayer | gLayer)) 
                {
                    //Debug.Log(hit.collider.name);

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
                        switch (mainGame)
                        {
                            case MainGame.hiddenSound: // 숨은 소리 찾기의 게임 오브젝트 인식 부분

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
                                gameObjectFunEvent.SSRaise(sound.objectNum, handCenter);

                                // 게임 오브젝트 주먹 인식
                                if (CognizeHandGesture(handGesture, 3.0f)) // 매개변수 3.0f 수정해서 원하는 초 만큼 주먹을 쥐어야 함수 실행 가능
                                {
                                    // 게임 오브젝트의 경우 해당 오브젝트가 선택되었을 때
                                    gameObjectFunEvent.Raise(sound.objectNum);
                                }

                                break;
                        }
                    }

                    preHit = hit;
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
        if (!audioSource.isPlaying || hit.collider.gameObject != preHit.collider.gameObject)
        {
            sound = hit.collider.GetComponent<Sound>();

            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = sound.cubeSound;
            audioSource.volume = 0.1f;
            audioSource.Play();

            rockTime = 0.0f;
        } 
    }

    // 소리 출력(UI와 같이 간격을 주고 반복해서 들려주는 오디오)
    private void PlaySound(float timer) // 매개변수 : 들려주는 간격을 몇 초로 줄 것인지.
    {

        if (audioSource.clip == null || hit.collider.gameObject != preHit.collider.gameObject)
        {
            sound = hit.collider.GetComponent<Sound>();

            audioSource.Stop();
            audioSource.volume = 0.1f;
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
    public void SetCurScene(MainGame gameName)
    {
        mainGame = gameName;
    }
}
