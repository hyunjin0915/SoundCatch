using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectFunctionEvent", menuName = "ScriptableObject/GameObjectFunctionEvent")]
public class GameObjectFunctionEvent : ScriptableObject
{
    private GameObjectEventListener listener;

    public void Raise(int objectIndex)
    {
        listener.OnEventRaised(objectIndex);
    }

    public void RegisterListener(GameObjectEventListener listener)
    {
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }
}
