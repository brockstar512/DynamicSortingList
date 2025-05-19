using System.Collections.Generic;
using System;
using UnityEngine;

public class EventMediator : MonoBehaviour
{
    // Singleton for easy access (optional)
    public static EventMediator Instance { get; private set; }

    private Dictionary<Type, Action<object>> eventTable = new Dictionary<Type, Action<object>>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Subscribe to an event
    public void Subscribe<T>(Action<T> callback)
    {
        Action<object> newAction = (obj) => callback((T)obj);

        if (eventTable.TryGetValue(typeof(T), out var existingAction))
        {
            eventTable[typeof(T)] = existingAction + newAction;
        }
        else
        {
            eventTable[typeof(T)] = newAction;
        }
    }

    // Unsubscribe from an event
    public void Unsubscribe<T>(Action<T> callback)
    {
        if (eventTable.TryGetValue(typeof(T), out var existingAction))
        {
            existingAction -= (obj) => callback((T)obj);
            if (existingAction == null)
                eventTable.Remove(typeof(T));
            else
                eventTable[typeof(T)] = existingAction;
        }
    }

    // Publish an event
    public void Publish<T>(T eventData)
    {
        if (eventTable.TryGetValue(typeof(T), out var action))
        {
            action.Invoke(eventData);
        }
    }
}