using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneLoader.Instance.ChangeScene("SelectGame");
        }
        else if(Input.GetMouseButtonDown(1))
        {
            SceneLoader.Instance.ChangeScene("SoundSource");
        }
    }
}
