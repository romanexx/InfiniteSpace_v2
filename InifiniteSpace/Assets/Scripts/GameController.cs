using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	SpaceShip_Controller m_Player; //We could try and make split screen co-op and make this and array but thats unneeded
	Spawn_Controller[] m_Spawners;

	int m_Score = 0;
	int m_Lives = 3;
	public float m_Respawn = 3.0f;
	float m_Timer; 

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_Spawners = FindObjectsOfType<Spawn_Controller>();
		m_Timer = m_Respawn;
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

		if(m_Player.M_health <= 0) 
		{
			m_Timer -= Time.deltaTime;
			if(m_Timer < 0.0f)
			{
				m_Player.Reset();
				m_Lives -= 1;
				m_Timer = m_Respawn;
			}
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

	//Getters and Setters for importiant variables.
	public int M_Score {
		get {
			return m_Score;
		}
		set {
			m_Score = value;
		}
	}

	public int M_Lives {
		get {
			return m_Lives;
		}
	}
}
