using UnityEngine;
using System.Collections;


public class WayPoint : MonoBehaviour {


	WayPoint_Manager myWaypointManager;
	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		myWaypointManager = FindObjectOfType<WayPoint_Manager>();
		if(!myWaypointManager)
		{
			Debug.LogError("You need to have a waypoint manager object in the scene");
		}
	}


	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "BaseEnemy")
		{
			Transform NextWaypoint = myWaypointManager.GetNextWaypoint(0);
			other.GetComponent<Enemy_Controller>().SetNextWaypoint(NextWaypoint);
		}
	}
}
