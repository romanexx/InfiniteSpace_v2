﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	SpaceShip_Controller m_Player; //We could try and make split screen co-op and make this and array but thats unneeded
	Spawn_Controller[] m_Spawners;

	int m_Score = 0;
	int m_Lives = 3;
	float m_RespawnTime = 3.0f;
	float m_timer = 0.0f;

	bool GameOver = false;

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

		if (m_Player.M_health <= 0) 
		{
			m_timer += Time.deltaTime;
			if(m_timer >= m_RespawnTime)
			{
				m_Player.Reset();
				m_Lives -= 1;
				if(m_Lives <= 0)
					GameOver = true;
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


	public int M_Score {
		get {
			return m_Score;
		}
		set {
			m_Score = value;
		}
	}
}
