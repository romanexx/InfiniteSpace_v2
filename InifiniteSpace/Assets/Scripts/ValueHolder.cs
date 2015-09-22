using UnityEngine;
using System.Collections;

public class ValueHolder : MonoBehaviour 
{
	string m_Username = "";

	public string M_Username {
		get {
			return m_Username;
		}
		set {
			m_Username = value;
		}
	}
}
