using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVisualController : Singleton<UIVisualController>
{
    public bool visual = true;

    public GameObject panel;

    public void visualChange()
    {
        panel.SetActive(!Instance.visual);
    }
}
