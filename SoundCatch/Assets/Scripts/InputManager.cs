using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public UIFunctionEvent uiFunEvent;
    public AudioSource guidevoiceSC;

    [SerializeField] private int uiNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            uiNum += 1;
            if(uiNum > 2)
            {
                uiNum = 0;
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            uiNum -= 1;
            if (uiNum < 0)
            {
                uiNum = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!guidevoiceSC.isPlaying)
            {
                uiFunEvent?.Raise(uiNum);
            }
        }
    }



}
