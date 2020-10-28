using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{

    #region Fields

    //Dictionary to hold the events possible in game
    protected Dictionary<EventNames , UnityEvent<AmmoType,float>> 
        events = new Dictionary<EventNames, UnityEvent<AmmoType,float>>();

    #endregion

    #region Methods

    //Add a listener for an event in the dicctionary
    public void AddListener(EventNames eventname, UnityAction<AmmoType,float> listener)
    {
        if (events.ContainsKey(eventname))
        {
            events[eventname].AddListener(listener);
        }
    }
    #endregion
}
