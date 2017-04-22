using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

	public float gravity = -10f;
	public float rotationSpeed = 50f;

	public void Attract(Transform body, Rigidbody2D rb) {
		Vector2 gravityUp = (body.position - transform.position).normalized;
		Vector2 bodyUp = body.up;

		rb.AddForce(gravityUp * gravity);

		Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp);
		body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}
