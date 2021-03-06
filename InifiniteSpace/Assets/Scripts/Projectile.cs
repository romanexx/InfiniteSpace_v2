﻿using UnityEngine;
using System.Collections;

public class Projectile : _BaseProjectile 
{
	public float minVol = 0.25f, maxVol = 1.0f;
	private AudioSource m_Audio;

	Rigidbody m_rigidbody;
	float life = 0.0f;

	public GameObject playerTransform;  // so we can pan the sound accordingly
	void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		m_Audio = GetComponent<AudioSource>();


		LifeTime += Time.time;
		Transform myTR = GetComponent<Transform>();


		if(playerTransform)
		{
			float posDiff =  (playerTransform.transform.position.x - myTR.position.x) / 100f;
			m_Audio.panStereo = posDiff;
			Debug.Log (posDiff);
		}
		m_Audio.volume = Random.Range(minVol,maxVol);
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
	}


}
