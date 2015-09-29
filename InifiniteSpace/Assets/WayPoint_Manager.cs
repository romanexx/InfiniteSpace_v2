using UnityEngine;
using System.Collections;


public class WayPoint_Manager : MonoBehaviour {

	public GameObject[] WayPoints;

	// Use this for initialization
	void Start () 
	{
		if(WayPoints == null)
			WayPoints =  GameObject.FindGameObjectsWithTag("WayPoint");

		if(WayPoints == null)
			Debug.LogError("There are no waypoints in the scene");
	}


	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log ("Length of the array is "+ WayPoints.Length);
		}
	}


	public Transform GetNextWaypoint(int index)
	{
		int newIndex = Random.Range(0,WayPoints.Length);
		//Debug.Log("Next WAYPOINT IS" + newIndex);
		return WayPoints[newIndex].transform;
	}

}
