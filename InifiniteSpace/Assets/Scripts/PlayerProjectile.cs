using UnityEngine;
using System.Collections;

public class PlayerProjectile : _BaseProjectile {

	
	public float minVol = 0.15f, maxVol = 0.7f;
	private AudioSource m_Audio;
	
	Rigidbody m_rigidbody;
	float life = 0.0f;
	
	//private AudioSource m_Audio;
	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_Audio = GetComponent<AudioSource>();
		
		m_Audio.volume = Random.Range(minVol,maxVol);
		LifeTime += Time.time;
		
	}
	
	void Update()
	{
		life = Time.time;
		if (life > LifeTime)
			Destroy(gameObject);
	}
	
	void FixedUpdate()
	{
		m_rigidbody.velocity = m_rigidbody.transform.forward * speed;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Radar")
			Destroy(gameObject);
		//Debug.Log ("New Sphere was destroyed");
	}
	

}
