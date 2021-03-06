﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_HUD : MonoBehaviour 
{
	SpaceShip_Controller m_Player = null;
	GameController m_Game = null;

	//Click and drag object refrences
	public UnityEngine.UI.Slider m_HealthBar;
	public Text m_Score;
	public Text m_Respawn;
	public Text m_Restart;
	public Text m_Help;
	public Text m_HSName;
	public Text m_HSScore;
	public RectTransform m_Lives;


	//Life Images control variables.
	float m_ImgWidth = 0;

	// Use this for initialization
	void Start () 
	{
		m_Player = FindObjectOfType<SpaceShip_Controller>();
		m_Game = FindObjectOfType<GameController>();

		//Setting the life counter up. 
		m_Lives.sizeDelta = new Vector2 (m_Lives.sizeDelta.x * m_Game.M_Lives, m_Lives.sizeDelta.y);
		m_ImgWidth = m_Lives.sizeDelta.x / (float)m_Game.M_Lives;

		m_Restart.enabled = false;
		m_HSName.enabled = false;
		m_HSScore.enabled = false;
		m_Help.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_Player != null) 
		{
			float healthRat = m_Player.M_health / m_Player.MaxHealth;
			m_HealthBar.value = m_HealthBar.maxValue * healthRat;
			m_Score.text = m_Game.M_Score.ToString();
			m_Lives.sizeDelta = new Vector2(m_ImgWidth * (float)m_Game.M_Lives, m_Lives.sizeDelta.y);

			if(m_Player.M_health <= 0)
			{
				m_Respawn.text = "Respawning " + m_Game.M_Timer.ToString();
			}
			else if(m_Game.M_Lives <= 0)
			{
				m_Respawn.text = "Game Over";
				m_Restart.enabled = true;

				m_HSName.text = "";
				m_HSScore.text = "";
				for(int i = 0; i < m_Game.M_HighScores.M_TopTen.Count; ++i)
				{
					m_HSName.text += (m_Game.M_HighScores.M_TopTen[i].s_Player + "\n");
					m_HSScore.text += (m_Game.M_HighScores.M_TopTen[i].s_Score + "\n");
				}
				m_HSName.enabled = true;
				m_HSScore.enabled = true;

			}
			else
			{
				m_Respawn.text = "";
				m_Restart.enabled = false;
				m_HSName.enabled = false;
				m_HSScore.enabled = false;
			}
		}
	}

	public void HelpOn()
	{
		m_Help.enabled = true;
	}

	public void HelpOff()
	{
		m_Help.enabled = false;
	}
}
