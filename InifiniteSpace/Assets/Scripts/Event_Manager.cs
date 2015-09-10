using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;



public class Event_Manager : MonoBehaviour
{
	private Dictionary<string,UnityEvent> eventDictionary;

	private static Event_Manager eventManager;

	public static Event_Manager instance
	{
		get
		{
			if(!eventManager)
			{
				eventManager = FindObjectOfType (typeof(Event_Manager)) as Event_Manager;

				if(!eventManager)
				{
					Debug.LogError("There needs to be one active Event_Manager script in your scene");
				}
				else
				{
					eventManager.Init();
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		if(eventDictionary == null)
		{
			eventDictionary = new Dictionary<string,UnityEvent >();
		}
	}

	public static void StartListening(string EventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if(instance.eventDictionary.TryGetValue(EventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(EventName,thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if(eventManager == null) return;

		UnityEvent thisEvent = null;

		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);

		}
	}

	public static void TriggerEvent(string eventName, GameObject invokingObject = null)
	{
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		  	{
				thisEvent.Invoke();
				
			}
	}

}


