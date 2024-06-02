using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public UIFunctionEvent uiFunEvent;
    public UIFunctionEvent uiOutlineEvent;
    public AudioSource guidevoiceSC;

    [SerializeField] private int uiNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if (uiNum != -1)
            {
                uiOutlineEvent?.Raise(uiNum + 3);
            }
            else
            {
                uiOutlineEvent?.Raise(3);
            }
            uiNum += 1;
            if(uiNum > 2)
            {
                uiNum = 0;
            }
            uiOutlineEvent?.Raise(uiNum);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (uiNum != -1)
            {
                uiOutlineEvent?.Raise(uiNum + 3);
            } else
            {
                uiOutlineEvent?.Raise(5);
            }
            uiNum -= 1;
            if (uiNum < 0)
            {
                uiNum = 2;
            }
            uiOutlineEvent?.Raise(uiNum);
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
