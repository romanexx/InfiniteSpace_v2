using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
//
public class ScoreTable
{
	public struct HighScore
	{
		public string s_Player;
		public int s_Score; 
	}

	List<HighScore> m_TopTen;



	// Use this for initialization
	public ScoreTable() 
	{
		m_TopTen = new List<HighScore>();
	}

	//Takes care of writeing to the xml 
	public void WriteScores()
	{
		XmlWriterSettings settings = new XmlWriterSettings
		{
			Indent = true,
			IndentChars = "  ",
			NewLineChars = "\r\n",
			NewLineHandling = NewLineHandling.Replace
		};

		XmlWriter xmlWriter = XmlWriter.Create(Application.dataPath + "/highScores.xml", settings);
		xmlWriter.WriteStartDocument ();
		xmlWriter.WriteStartElement ("HighScores");

		for (int i = 0; i < m_TopTen.Count; ++i) 
		{
			xmlWriter.WriteStartElement("highscore");
			xmlWriter.WriteAttributeString("player", m_TopTen[i].s_Player);
			xmlWriter.WriteAttributeString("score", m_TopTen[i].s_Score.ToString());
			xmlWriter.WriteEndElement();
		}
		xmlWriter.WriteEndElement();
		xmlWriter.Close();
	}

	//takes care of reading from the xml
	public void LoadScores()
	{
		XmlReader xmlReader = XmlReader.Create (Application.dataPath + "/highScores.xml");
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load (xmlReader);
		foreach(XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
		{
			HighScore tempHS = new HighScore();
			tempHS.s_Player = xmlNode.Attributes["player"].Value;
			tempHS.s_Score =  int.Parse(xmlNode.Attributes["score"].Value);
			m_TopTen.Add(tempHS);
		}
		xmlReader.Close();
	}

	//Checks if you're score is worthy of a podium.
	public bool CheckScore(int score)
	{
		if (m_TopTen.Count < 10)
			return true;
		else 
		{
			for(int i = 0; i < m_TopTen.Count; ++i)
			{
				if(score > m_TopTen[i].s_Score)
					return true;
			}
			return false; 
		}
	}

	//Adds the new score to the list.
	public int NewScore(string name, int score)
	{
		HighScore nHS = new HighScore();
		nHS.s_Player = name;
		nHS.s_Score = score;

		if (m_TopTen.Count < 10) 
		{
			for(int i = 0; i < m_TopTen.Count; ++i)
			{
				if(nHS.s_Score > m_TopTen[i].s_Score)
				{
					m_TopTen.Insert(i, nHS);
					return i+1;
				}
				else if( i+1 >= m_TopTen.Count)
				{
					m_TopTen.Add(nHS);
					return i+1;
				}
			}
		}
		else
		{
			for(int i = 0; i < m_TopTen.Count; ++i)
			{
				if(nHS.s_Score > m_TopTen[i].s_Score)
				{
					m_TopTen.Insert(i, nHS);
					m_TopTen.Remove(m_TopTen[m_TopTen.Count-1]);
					return i+1;
				}
			}
		}

		return -1;
	}


	//Getters and setters for the highscores. 
	public List<HighScore> M_TopTen {
		get {
			return m_TopTen;
		}
		set {
			m_TopTen = value;
		}
	}
}
