using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn_Controller : MonoBehaviour 
{
	public GameObject object_type;
	
	public int spawn_count;
	public float spawn_rate;
	
	int currentCount = 0;
	float nextSpawn = 0.0f;
	
	Transform m_Transform;

	List<GameObject> my_Ships;

	void OnEnable()
	{
		//Event_Manager.StartListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy+= UpdateTargets;

	}
	
	void OnDisable()
	{
		//Event_Manager.StopListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy-= UpdateTargets;
	}
	
	void UpdateTargets(GameObject obj = null)
	{
		if(my_Ships.Contains(obj))
		{
			my_Ships.Remove(obj);
			currentCount-=1;
		}
			
	}





	// Use this for initialization
	void Start () 
	{
		m_Transform = GetComponent<Transform>();
		my_Ships = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > nextSpawn && currentCount < spawn_count) 
		{
			currentCount += 1;
			nextSpawn = Time.time + spawn_rate;
			GameObject g = Instantiate (object_type, m_Transform.position, m_Transform.rotation) as GameObject;
			my_Ships.Add(g);

			Transform NextWaypoint = WayPoint_Manager.GetNextWaypoint(0);
			g.GetComponent<Enemy_Controller>().SetNextWaypoint(NextWaypoint);
		}
	}
	
	//Spawns one enemy of the type associated with the spawn point.
	public void Spawn()
	{
		// Not sure if this is being used----RT 9-21-15
		Instantiate (object_type, m_Transform.position, m_Transform.rotation);
	}

	public void Reset()
	{
		currentCount = 0;
		nextSpawn = 0.0f;
	}
}
