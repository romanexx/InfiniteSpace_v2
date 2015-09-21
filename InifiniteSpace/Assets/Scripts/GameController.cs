using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	SpaceShip_Controller m_Player; //We could try and make split screen co-op and make this and array but thats unneeded
	Player_HUD m_HUD;
	Spawn_Controller[] m_Spawners;
	ScoreTable m_HighScores;

	int m_Score = 0;
	int m_Lives = 3;
	public float m_Respawn = 3.0f;
	float m_Timer;

	bool m_bHighScore = false;

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_HUD = FindObjectOfType<Player_HUD>();
		m_Spawners = FindObjectsOfType<Spawn_Controller>();

		m_HighScores = new ScoreTable();
		m_HighScores.LoadScores ();
		m_Timer = m_Respawn;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Lives > 0) 
		{
			if (Input.GetKeyUp ("tab")) {
				if (m_Spawners.Length > 0)
					m_Spawners [0].Spawn ();
			}
			
			if (Input.GetKeyDown ("f1"))
				m_HUD.HelpOn ();
			if (Input.GetKeyUp ("f1"))
				m_HUD.HelpOff ();

			if (m_Player.M_health <= 0) 
			{
				m_Timer -= Time.deltaTime;
				if (m_Timer < 0.0f) {
					m_Lives -= 1;
					m_Player.Reset ();
					m_Timer = m_Respawn;
				}
			}
		} 
		else 
		{
			m_bHighScore = m_HighScores.CheckScore(m_Score);

			if(Input.GetKeyUp (KeyCode.Return))
				Restart();
		}

		if (Input.GetKeyUp ("escape")) 
		{
			//Do pause menu logic
			Application.LoadLevel ("MainMenu");
		}

	}

	void Restart()
	{
		m_Lives = 3;
		for (int i = 0; i < m_Spawners.Length; ++i) 
		{
			m_Spawners[i].Reset();
		}
		m_Player.Reset();
		m_Timer = m_Respawn;
		m_Score = 0;
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

	public float M_Timer {
		get {
			return m_Timer;
		}
	}

	public bool M_bHighScore {
		get {
			return m_bHighScore;
		}
	}
}
