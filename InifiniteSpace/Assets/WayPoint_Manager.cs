using UnityEngine;
using System.Collections;


public class WayPoint_Manager : MonoBehaviour {

	public static GameObject[] WayPoints;

	// Use this for initialization
	void Awake () 
	{
		if(WayPoints == null)
			WayPoints =  GameObject.FindGameObjectsWithTag("WayPoint");
	}


	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log ("Length of the array is "+ WayPoints.Length);
		}
	}


	public static Transform GetNextWaypoint(int index)
	{
		int newIndex = Random.Range(0,WayPoints.Length);
		//Debug.Log("Next WAYPOINT IS" + newIndex);
		return WayPoints[newIndex].transform;
	}

}
