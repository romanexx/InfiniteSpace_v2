using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Radar : _BaseRadar {

	List<GameObject> Targets;


	public Transform player;
	Transform myTransform;
	// Use this for initialization
	void Start () 
	{
		Targets = new List<GameObject>();
		myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		myTransform.position = player.transform.position;


		float Scroll = Input.GetAxis("Mouse ScrollWheel");
		if(Scroll > 0f)
		{
			Debug.Log("Switching Targets- There are " + Targets.Count );
		}
		if(Scroll < 0f)
		{
			Debug.Log("Switching Targets- There are " + Targets.Count );
			var t = Targets.GetEnumerator();

		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == TagToDetect)
		{
			if(GetTarget()==null)
			{
				SetTarget(other.gameObject);
				Targets.Add(other.gameObject);
			}
			else if(Targets.Contains(other.gameObject)==false)
			{
				Targets.Add(other.gameObject);

			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == TagToDetect)
		{
			if(Targets.Contains(other.gameObject))
			{
				Targets.Remove(other.gameObject);
			}
		}

	}


}
