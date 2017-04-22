using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

	FauxGravityAttractor attractor;

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		GameObject g = GameObject.FindGameObjectWithTag("Planet");
		attractor = g.GetComponentInChildren<FauxGravityAttractor>();


		rb = this.GetComponentInChildren<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		rb.gravityScale = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		attractor.Attract(transform, rb);
	}
}
