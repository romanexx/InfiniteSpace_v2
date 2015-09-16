using UnityEngine;
using UnityEngine.Events;
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

	void OnEnable()
	{
		//Event_Manager.StartListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy+= UpdateTargets;
	}

	void OnDisable()
	{
		//Event_Manager.StopListening("EnemyDestroyed", UpdateTargets);
		Event_System.OnEnemyDestroy-= UpdateTargets;
	}

	void UpdateTargets(GameObject obj = null)
	{
		if(Targets.Contains(obj))
			Targets.Remove(obj);


		if(GetTarget() == obj)     //if my current target died lets pick a new one
		{
			if(Targets.Count > 0)  // if there are more targets in our list- just set current target to the first one in the list
				SetTarget(Targets[0]);
			else
				SetTarget(null);
		}

	}
	public Texture tex;

	void OnGUI()
	{

		if(!GetTarget())
			return;

		Transform t = GetTarget().transform;

		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (t.position);
		
		GUI.Box(new Rect(targetPos.x-15, Screen.height- targetPos.y-15, 25, 25), tex);


	
		
	}


	// Update is called once per frame
	void Update () 
	{

		myTransform.position = player.transform.position;

	



		float Scroll = Input.GetAxis("Mouse ScrollWheel");
		if (Targets.Count > 1) 
		{

			int NumTargets = Targets.Count;
			int TargetIndex = Targets.IndexOf (GetTarget ());

			if (Scroll > 0f) 
			{
				++TargetIndex;     //Go to next Target
			}
			if (Scroll < 0f) 
			{
				--TargetIndex;     // Go To Prev Target
			}


			if(TargetIndex > NumTargets-1)
				TargetIndex = 0;
			else if(TargetIndex < 0)
				TargetIndex = NumTargets-1;


			SetTarget(Targets[TargetIndex]);

			if(GetTarget())
			{
				Debug.DrawRay(myTransform.position, GetTarget().transform.position, Color.green);
				
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == TagToDetect)
		{

			if(Targets.Contains(other.gameObject)==false)
			{
				Targets.Add(other.gameObject);

				if(GetTarget() == null)
					SetTarget(other.gameObject);

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

			if(GetTarget() == other.gameObject)
			{
				if(Targets.Count > 0)  // if there are more targets in our list- just set current target to the first one in the list
					SetTarget(Targets[0]);
				else
					SetTarget(null);
			}
		}

	}


}
