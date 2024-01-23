using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprashProjectile : Projectile
{
	public float curveSpeed = 5f; // Adjust the speed of the curve movement
	public GameObject SprashPrefab;
	public float WaitTime = 4f;

	private Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		// Apply initial velocity in the up direction
		rb.velocity = transform.up * curveSpeed;
	
	
	}

	void OnBecameInvisible()
	{
		// Optionally destroy the projectile or handle other logic
		Destroy(gameObject);
	}
}
