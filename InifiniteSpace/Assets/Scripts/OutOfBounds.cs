using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	// Use this for initialization
	public float MaxBounds = 400f;


	SphereCollider bounds;
	void Start () 
	{
		bounds = GetComponent<SphereCollider>();
		bounds.radius = MaxBounds;
	}
	
	void OnTriggerExit(Collider other)
	{
		IControllable t = other.GetComponent<IControllable>();

		//Debug.Log (other.tag + "Collided with gamebounds");

		if(t!= null)
		{
			t.ControlOverride(false);
			//Debug.Log (other.tag + "CONTROL OVERRIDE");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		IControllable t = other.GetComponent<IControllable>();

		//Debug.Log (other.tag + "Collided with gamebounds");
		if(t!=null)
		{
			t.ControlOverride(true);
		}
	}

}
