using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject

{
    private readonly List<GameEventListener> eventListeners =
        new List<GameEventListener>();

    public void Raise(Component sender, object data) 
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(sender, data);
    }

    public void RegisterListener(GameEventListener listener) 
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) 
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
