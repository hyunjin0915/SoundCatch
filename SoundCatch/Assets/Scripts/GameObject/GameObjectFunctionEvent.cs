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

    // 소리원 찾기 게임에서 필요해서 추가
    public void SSRaise(Vector3 handPos)
    {
        listener.OnSSEventRaised(handPos);
    }

    // 음 맞추기 게임에서 필요해서 추가
    public void TSRaise(Vector3 handPos)
    {
        listener.OnTSEventRaised();
    }
}
