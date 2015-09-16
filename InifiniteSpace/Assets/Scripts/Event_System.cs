using UnityEngine;
using System.Collections;

public class Event_System : MonoBehaviour 
{

	public delegate void EventHandler(GameObject obj);

	public static event EventHandler OnEnemyDestroy;

	public static void EnemyDestroyed(GameObject obj)
	{
		if(OnEnemyDestroy!= null)
			OnEnemyDestroy(obj);
	}
}
