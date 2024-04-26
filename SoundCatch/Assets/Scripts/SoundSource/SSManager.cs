using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSManager : MonoBehaviour
{
    public GameObject soundSource;
    public AudioSource audioSource;
    private Vector3 ssPos;
    public GameObject[] obstacles;

    private float maxSize = 10.3f;

    private Vector2 handPos;

    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("HTManager").GetComponent<AudioSource>();
        ssPos = new Vector3(Random.Range(-4.5f, 5.2f), Random.Range(1.0f, 5.6f), 2.5f);
        soundSource.transform.localPosition = ssPos;
    }

    public void CheckSound(int objectNum, Vector3 handPos)
    {
        switch (objectNum)
        {
            case 0:
                audioSource.volume = CalDis(handPos);
                break;
            case 1:
                audioSource.volume = 0.1f;
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    public void CheckAnswer(int objectNum)
    {
        if (objectNum == 1)
        {
            Debug.Log("Clear"); // 임의 작성
        }
    }

    private float CalDis(Vector3 handPos)
    {
        this.handPos = new Vector2(handPos.x, handPos.y);
        float dis = Vector2.Distance(this.handPos, ssPos) - 0.4f;

        if (dis > maxSize)
        {
            return 0.01f;
        }
        else if (dis > 0.03f)
        {
            return 0.09f * (1f - Mathf.Clamp01(dis / maxSize));
        } else
        {
            return 0.1f;
        }
    }
}
