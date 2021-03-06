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

	void ControlOverride(bool val)
	{

	}

	public WayPoint_Manager myWaypointManager;
	Transform MyWaypoint = null;
	public void SetNextWaypoint(Transform val)
	{
		MyWaypoint = val;
	}
	void FlyToWaypoint()
	{
		m_transform.rotation = Quaternion.Slerp(m_transform.rotation, Quaternion.LookRotation(MyWaypoint.position- m_transform.position),m_turnRate* Time.deltaTime);
		m_rigidbody.velocity = m_transform.forward * m_CurrSpeed;
	}

	/****************************************************************/
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

	public GameObject m_Death;


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

		myWaypointManager = FindObjectOfType<WayPoint_Manager>();
		if(!myWaypointManager)
		{
			Debug.LogError("You need to have a waypoint manager object in the scene");
		}
	}

	void Update()
	{
		if (m_heatlh <= 0)
		{
			//Event_Manager.TriggerEvent("EnemyDestroyed");
			Event_System.EnemyDestroyed(this.gameObject);
			Object death = Instantiate(m_Death, m_rigidbody.position, m_rigidbody.rotation);
			Destroy(death, 1.0f);
			Destroy (gameObject);
		}

		if (myRadar.GetTarget()) 
		{
			//Checks if the Player is infront of the enemy and if they're in a sutible range to attack
			var m_HitInfo = new RaycastHit ();
			if (Physics.Raycast (m_transform.position, m_transform.forward, out m_HitInfo, m_fireRange) == true) {
				if (m_HitInfo.collider.tag == "Player") {
					for (int i = 0; i < m_lasers.Length; ++i)
						m_lasers [i].Fire ();
			
				}
			
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
					m_CurrSpeed = m_MaxSpeed * 0.75f;
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
			if(MyWaypoint == null)
			{
				m_transform.Rotate (0f, m_turnRate, 0f);
				m_rigidbody.velocity = m_transform.forward * m_CurrSpeed;
			}
			else
				FlyToWaypoint();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BaseProjectile") 
		{
			 TakeDamage(other.GetComponent<_BaseProjectile>().Damage);
		}

	}

	public void TargetAcquired(bool b)
	{
		m_BaseBehavior = b;
	}



}
