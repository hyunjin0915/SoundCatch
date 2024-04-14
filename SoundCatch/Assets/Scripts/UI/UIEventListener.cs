using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEventListener : MonoBehaviour
{
    public UIFunctionEvent Event;
    public List<UnityEvent> ResponseList;

    public void OnEventRaised(int index)
    { 
        ResponseList[index].Invoke(); 
    }

    private void OnEnable()
    { 
        Event.RegisterListener(this); 
    }

    private void OnDisable()
    { 
        Event.UnregisterListener(); 
    }
}
