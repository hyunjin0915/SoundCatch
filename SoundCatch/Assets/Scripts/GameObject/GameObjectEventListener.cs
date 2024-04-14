using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventListener : MonoBehaviour
{
    public GameObjectFunctionEvent Event;
    public UnityEvent<int> Response;

    public void OnEventRaised(int objectIndex)
    { 
        Response.Invoke(objectIndex); 
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
