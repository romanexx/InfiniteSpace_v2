using UnityEngine;
using System.Collections;

public class MainMenuContorller : MonoBehaviour 
{

	public void StartButton()
	{
		Application.LoadLevel ("Level_1");
	}

	public void ExitButton()
	{
		Application.Quit ();
	}
}
