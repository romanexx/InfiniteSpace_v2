﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuContorller : MonoBehaviour 
{
	Button[] m_Buttons; 

	public Text m_Title;
	public Text m_Credits; 
	float m_CreYPos = 0.0f; //Default Position for the credits.
	public InputField m_IF;
	public Text Warning;
	public ValueHolder m_Values;


	void Start()
	{
		m_Title.enabled = true;
		m_Credits.enabled = false;
		m_CreYPos = m_Credits.rectTransform.position.y;
		m_Buttons = FindObjectsOfType<Button>();
		Warning.enabled = false;
	}

	void Update()
	{
		m_Values.M_Username = m_IF.text;

		if ((m_Credits.rectTransform.position.y > 100.0f || Input.GetKey(KeyCode.Return) || Input.GetKey("escape")) && m_Credits.enabled == true) 
		{
			m_Credits.enabled = false; 
			m_Credits.rectTransform.position = new Vector3(m_Credits.rectTransform.position.x, m_CreYPos, m_Credits.rectTransform.position.z);
			for (int i = 0; i < m_Buttons.Length; ++i) 
			{
				m_Buttons[i].gameObject.SetActive(true);
			}
			m_IF.gameObject.SetActive(true);
			m_Title.enabled = true;
		}
	}

	void FixedUpdate()
	{
		if (m_Credits.enabled == true) 
		{
			m_Credits.rectTransform.position = new Vector3(m_Credits.rectTransform.position.x, m_Credits.rectTransform.position.y + 0.25f, m_Credits.rectTransform.position.z);
		}
	}

	public void StartButton()
	{
		if (m_IF.text != "") 
		{
			DontDestroyOnLoad (m_Values);
			Application.LoadLevel ("Level_1");
		} 
		else 
		{
			Warning.enabled = true;
		}
	}

	public void Credits()
	{
		m_Credits.enabled = true;
		for (int i = 0; i < m_Buttons.Length; ++i) 
		{
			m_Buttons[i].gameObject.SetActive(false);
		}
		m_IF.gameObject.SetActive(false);
		m_Title.enabled = false;
	}

	public void ExitButton()
	{
		Application.Quit ();
	}
}
