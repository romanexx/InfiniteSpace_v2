using UnityEngine;
using System.Collections;

public class TargetDesignator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}




	public Transform target;
	
	void Update ()
	{
		Vector3 wantedPos = Camera.main.WorldToViewportPoint (target.position);
		transform.position = wantedPos;
	}


}
