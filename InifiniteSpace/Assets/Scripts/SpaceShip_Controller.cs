﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceShip_Controller : MonoBehaviour, IDamageable<int> {

	//interface methods
	public void TakeDamage(int dam)
	{
		m_health -= dam;
	}


	public int MaxHealth;
	float m_health;
    Rigidbody m_rigidbody;
    public GameObject playerModel;
    Transform m_Transform;
    public float Tilt;
    public float Speed;

	public GameObject m_Target; 



	public Player_Radar myRadar;
	Missile_HardPoint [] missiles; // holds the two spawn points for the missiles
	Laser_Hardpoint [] lasers;	   // holds all the laser spawn points - array in case we want multiple lasers
	public GameObject m_Missile;  // holds the prefab for missile
	public GameObject getMissilePrefab(){return m_Missile;}// Accessor for the missile prefab, in case we wanna have different kinds of missiles;

	public GameObject Laser;
	public GameObject getLaserPrefab(){return Laser;}  //  Accessor for the laser prefab, in case we wanna have different kinds of lasers;

	Renderer m_ship;


	// Use this for initialization
	void Start () 
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_Transform = GetComponent<Transform>();
		missiles = GetComponentsInChildren<Missile_HardPoint>();
		lasers = GetComponentsInChildren<Laser_Hardpoint>();
		m_ship = GetComponentInChildren<Renderer>();
		m_health = MaxHealth;
	}

	int nextmissile = 0;
	void Update()
	{
		if(m_health > 0) 
		{
			if (Input.GetButton ("Fire1")) {
				lasers [0].Fire ();
			
			}
			
			
			if (Input.GetButtonDown ("Fire2")) {
				// Fire Missile
			
				bool b = missiles [nextmissile].Fire (myRadar.GetTarget ());
				if (b)
					nextmissile = nextmissile ^ 1;
				//Debug.Log(nextmissile);
			}
		} 
		else 
		{
			if(m_ship != null)
				m_ship.enabled = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
    {
		if (m_health > 0) 
		{
			float fVertical = Input.GetAxis ("Vertical");
			float fHorizontal = Input.GetAxis ("Horizontal");

			//Vector3 move = new Vector3(0.0f,0.0f, fVertical);

			m_Transform.Rotate (0f, fHorizontal, 0f);
			m_rigidbody.velocity = m_Transform.forward * Speed * fVertical;



			if (fVertical >= 0f)
				playerModel.transform.localRotation = Quaternion.Euler (0f, 0f, fHorizontal * -Tilt);
			else
				playerModel.transform.localRotation = Quaternion.Euler (0f, 0f, -fHorizontal * -Tilt);
		}
		else 
		{
			m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}

	public void Reset()
	{
		m_health = MaxHealth;
		m_Transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		m_Transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		m_ship.enabled = true;
	}
	
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "BaseProjectile") 
		{
			TakeDamage(other.GetComponent<Projectile>().Damage);
		}

		if(other.tag == "BaseEnemy")
		{
			TakeDamage (200);
			Event_System.EnemyDestroyed(this.gameObject);
			Destroy(other.gameObject);
		}

	}
	
	public float M_health {
		get {
			return m_health;
		}
		set {
			m_health = value;
		}
	}
}
