using UnityEngine;
using System.Collections;

public class MainMenuContorller : MonoBehaviour 
{

	public void StartButton()
	{
		Application.LoadLevel ("FirstTestScene");
	}

	public void ExitButton()
	{
		Application.Quit ();
	}
}
