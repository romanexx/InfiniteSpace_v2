using UnityEngine;
using System.Collections;

public class PlanetRotate : MonoBehaviour {



	public float Rate = 2f;
	Transform myTransform;
	public Vector3 vec;
	public bool RandomRotation = false;
	// Use this for initialization
	void Start () 
	{
		myTransform = GetComponent<Transform>();
		if(RandomRotation)
			vec = Random.insideUnitSphere;
	}
	
	// Update is called once per frame
	void Update () 
	{


		myTransform.Rotate(vec * Rate * Time.deltaTime);

		//myTransform.Rotate(
	}
}
