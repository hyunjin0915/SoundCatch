using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventListener : MonoBehaviour
{
    public GameObjectFunctionEvent Event;
    public UnityEvent<int> Response;

    public UnityEvent<Vector3> ssResponse;
    public UnityEvent tsResponse;

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

    // 소리원 찾기 게임에서 필요해서 추가
    public void OnSSEventRaised(Vector3 handPos)
    {
        ssResponse.Invoke(handPos);
    }

    // 음 맞추기 게임에서 필요해서 추가
    public void OnTSEventRaised()
    {
        tsResponse.Invoke();
    }
}
