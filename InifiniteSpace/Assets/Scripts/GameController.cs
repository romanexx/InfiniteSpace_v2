using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	SpaceShip_Controller m_Player; //We could try and make split screen co-op and make this and array but thats unneeded
	Enemy_Spawn[] m_Spawners;


	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_Spawners = FindObjectsOfType<Enemy_Spawn>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp("tab")) 
		{
			if(m_Spawners.Length > 0)
				m_Spawners[0].SpawnEnemy();
		}
	}
}
