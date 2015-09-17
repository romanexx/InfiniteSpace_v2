using UnityEngine;
using System.Collections;

public class Spawn_Controller : MonoBehaviour 
{
	public GameObject object_type;
	
	public int spawn_count;
	public float spawn_rate;
	
	int currentCount = 0;
	float nextSpawn = 0.0f;
	
	Rigidbody m_rigidbody;
	
	// Use this for initialization
	void Start () 
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > nextSpawn && currentCount < spawn_count) 
		{
			currentCount += 1;
			nextSpawn = Time.time + spawn_rate;
			Instantiate (object_type, m_rigidbody.position, m_rigidbody.rotation);
		}
	}
	
	//Spawns one enemy of the type associated with the spawn point.
	public void Spawn()
	{
		Instantiate (object_type, m_rigidbody.position, m_rigidbody.rotation);
	}

	public void Reset()
	{
		currentCount = 0;
		nextSpawn = 0.0f;
	}
}
