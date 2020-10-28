using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager 
{


    static bool initialized = false;


    //Dictionaries to hold listeners and invokers
    static Dictionary<EventNames, List <EventInvoker>> 
        invokers = new Dictionary<EventNames, List<EventInvoker>>();

    static Dictionary<EventNames, List<UnityAction
        <AmmoType,float>>> listeners = new Dictionary<EventNames, List<UnityAction<AmmoType,float>>>();



    //Makes sure that dictionaries have empty lists for every
    //event when the game starts.

    public static void Initialize()
    {
        if (!initialized)
        {
            initialized = true;
            foreach (EventNames name in Enum.GetValues(typeof(EventNames)))
            {
                invokers[name] = new List<EventInvoker>();
                listeners[name] = new List<UnityAction<AmmoType, float>>();
            }
        }
        else
        {
            foreach(EventNames name in Enum.GetValues(typeof(EventNames)))
            {
                invokers[name].Clear();
                listeners[name].Clear();
            }
        }
    }



    /// <summary>
    /// Adds the given script as an invoker 
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="invoker"></param>
    static public void AddInvoker(EventNames eventName,EventInvoker invoker)
    {
        foreach(UnityAction<AmmoType,float> listener in listeners[eventName])
        {
            invoker.AddListener(eventName, listener);
        }
        invokers[eventName].Add(invoker);
    }




    /// <summary>
    /// Adds the given listener in the list of listeners for an event
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    static public void AddListener(EventNames eventName,UnityAction<AmmoType,float> listener)
    {
        foreach(EventInvoker invoker in invokers[eventName])
        {
            invoker.AddListener(eventName, listener) ;
        }
        listeners[eventName].Add(listener);
    }
}
