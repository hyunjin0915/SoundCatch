using UnityEngine;

[CreateAssetMenu(fileName = "UIFunctionEvent", menuName = "ScriptableObject/UIFunctionEvent")]
public class UIFunctionEvent : ScriptableObject
{
    private UIEventListener listener;

    public void Raise(int index)
    {
        listener.OnEventRaised(index);
    }

    public void RegisterListener(UIEventListener listener)
    {
        this.listener = listener;
    }

    public void UnregisterListener()
    {
        listener = null;
    }
}
