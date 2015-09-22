using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "BaseEnemy")
		{
			Transform NextWaypoint = WayPoint_Manager.GetNextWaypoint(0);
			other.GetComponent<Enemy_Controller>().SetNextWaypoint(NextWaypoint);
		}
	}
}
