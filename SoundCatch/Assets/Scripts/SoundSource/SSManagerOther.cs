using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSManagerOther : MonoBehaviour
{
    public AudioClip[] clips;
    public Sound[] objects;
    private float[] volumes = { 0.2f, 0.1f, 0.4f, 0.3f, 0.4f, 0.3f, 0.2f, 0.1f, 0.8f };

    public int answerNum = 0;
    public int clipNum = 0;

    private void Start()
    {
        answerNum = Random.Range(0, 10);
        clipNum = Random.Range(0, 13);
        SuffleVoluems();
        Swap(answerNum, volumes.Length - 1);

        for (int i = 0; i < 9; i++)
        {
            objects[i].cubeSound = clips[clipNum];
            objects[i].volume = volumes[i];
        }
    }

    public void CheckAnswer(int objectNum)
    {
        if (answerNum == objectNum)
        {
            Debug.Log("Clear"); // 임의 작성
        }
    }

    private void SuffleVoluems()
    {
        int n = volumes.Length - 1;
        
        for (int i = 0; i < n - 1; i++)
        {
            int r = Random.Range(i, n);

            Swap(i, r);
        }
    }

    private void Swap(int a, int b)
    {
        float temp = volumes[a];
        volumes[a] = volumes[b];
        volumes[b] = temp;
    }
}
