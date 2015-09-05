using UnityEngine;
using System.Collections;

public class Player_HUD : MonoBehaviour 
{
	SpaceShip_Controller m_Player = null;

	//Health Bar information
	float m_startHealth;
	public UnityEngine.UI.Slider m_HealthBar;

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_startHealth = m_Player.Health;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Player != null) 
		{
			float healthRat = m_Player.Health / m_startHealth;
			m_HealthBar.value = m_HealthBar.maxValue * healthRat;
		}
	}
}
