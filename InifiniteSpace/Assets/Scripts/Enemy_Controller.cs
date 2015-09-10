using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Controller : MonoBehaviour, IDamageable<int> 
{
	//interface Methods
	public void TakeDamage(int dam)
	{
		m_heatlh -=dam;
	}



	Rigidbody m_rigidbody;
	Transform m_transform;

	Enemy_Radar myRadar;

	//General Enemy Attributes
	public int m_heatlh;
	public float m_MaxSpeed;
	float m_CurrSpeed;
	public float m_turnRate;
	public float m_fireRange;
	public bool m_BaseBehavior = false;


	//Ai Behavior stuff.
	Vector3 m_ToTarget;
	Laser_Hardpoint[] m_lasers;

	//All the Variables needed for firing projectiles. 
	public GameObject Laser;
	public GameObject getLaserPrefab()
	{
		return Laser;  //  Accessor for the laser prefab, in case we wanna have different kinds of lasers;
	}

	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_transform = GetComponent<Transform>();

		//Gives the Enemy its laser spawn poitions.
		m_lasers = GetComponentsInChildren<Laser_Hardpoint>();
		myRadar = GetComponentInChildren<Enemy_Radar>();
		m_CurrSpeed = m_MaxSpeed;
	}

	void Update()
	{
		if (m_heatlh <= 0)
			Destroy (gameObject);

		//Checks if the Player is infront of the enemy and if they're in a sutible range to attack
		var m_HitInfo = new RaycastHit();
		if (Physics.Raycast (m_transform.position, m_transform.forward, out m_HitInfo, m_fireRange) == true) 
		{
			if(m_HitInfo.collider.tag == "Player")
			{
				for(int i = 0; i < m_lasers.Length; ++i)
					m_lasers[i].Fire();
				//Debug.Log("Enemy Firing");
			}

		}
	}

	void FixedUpdate()
	{
		if (m_BaseBehavior == true) 
		{
			if(myRadar.GetTarget())
			{
				m_ToTarget = myRadar.GetTarget().transform.position - m_transform.position;
				Vector3 newDir;
				float step = m_turnRate * Time.deltaTime;
				//Ships behavior when chasing the player. 
				if(m_ToTarget.magnitude < 50.0f)
				{
					//Avoid Crashing into the player
					m_CurrSpeed = m_MaxSpeed * 0.5f;
					newDir = Vector3.RotateTowards(m_transform.forward, -m_ToTarget, step, 0.0f);
				}
				else
				{
					//Drive striaght at the enemy guns blazing. 
					m_CurrSpeed = m_MaxSpeed;
					newDir = Vector3.RotateTowards(m_transform.forward, m_ToTarget, step, 0.0f);
				}

				m_transform.rotation = Quaternion.LookRotation(newDir);
				m_rigidbody.velocity = m_transform.forward * m_CurrSpeed;
			} 

		}
		else 
		{
			//If there is no target it does a base patrol;
			m_transform.Rotate (0f, m_turnRate, 0f);
			m_rigidbody.velocity = m_transform.forward * m_CurrSpeed;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BaseProjectile") 
		{
			 TakeDamage(other.GetComponent<Projectile>().Damage);
		}
//		else if(other.tag == "Radar")
//		{
//			Debug.Log("Collided with the radar");
//		}
//		else if (other.tag == "Player") 
//		{
//			Debug.Log("Collided with the player");
//			Destroy(gameObject);
//		}
	}

	public void TargetAcquired(bool b)
	{
		m_BaseBehavior = b;
	}
}
