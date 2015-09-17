using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_HUD : MonoBehaviour 
{
	SpaceShip_Controller m_Player = null;
	GameController m_Game = null;

	//Health Bar information
	float m_startHealth;
	public UnityEngine.UI.Slider m_HealthBar;

	public Text m_Score;

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_Game = FindObjectOfType<GameController>();
		m_startHealth = m_Player.Health;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Player != null) 
		{
			float healthRat = m_Player.Health / m_startHealth;
			m_HealthBar.value = m_HealthBar.maxValue * healthRat;
			m_Score.text = m_Game.M_Score.ToString();


		}
	}
}
