using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	SpaceShip_Controller m_Player; //We could try and make split screen co-op and make this and array but thats unneeded
	Spawn_Controller[] m_Spawners;

	int m_Score = 0; 

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_Spawners = FindObjectsOfType<Spawn_Controller>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp("tab")) 
		{
			if(m_Spawners.Length > 0)
				m_Spawners[0].Spawn();
		}

		if (Input.GetKeyUp ("escape")) 
		{
			//Do pause menu logic
			Application.LoadLevel("MainMenu");
		}
	}

	//Message Shit
	void OnEnable()
	{
		//Event_Manager.StartListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy += IncrementScore;
	}
	
	void OnDisable()
	{
		//Event_Manager.StopListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy -= IncrementScore;
	}

	//Updating Score
	void IncrementScore(GameObject enemy)
	{
		m_Score += 10;
	}


	public int M_Score {
		get {
			return m_Score;
		}
		set {
			m_Score = value;
		}
	}
}
