using UnityEngine;
using System.Collections;

public class _BaseProjectile : MonoBehaviour {

	public float speed = 100f;
	public float LifeTime = 2f;
	public int damage = 25;



	public int Damage 
	{
		get {
			return damage;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
