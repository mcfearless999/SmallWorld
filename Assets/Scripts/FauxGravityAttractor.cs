using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour {

	public float gravity = -10f;
	public float rotationSpeed = 50f;

	public float targetRotation;

	public List<GameObject> objects = new List<GameObject>();

	public void Attract(Transform body, Rigidbody2D rb) {
		Vector2 gravityUp = (body.position - transform.position).normalized;
		Vector2 bodyUp = body.up;

		rb.AddForce(gravityUp * gravity);

		//Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp);

		targetRotation = -Mathf.Atan2(body.position.y, body.position.x) * Mathf.Rad2Deg + 90f; // For other objects
		//body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		//body.rotation = Quaternion.Euler( new Vector3(0f, 0f, targetRotation));
		//body.Rotate(new Vector3(0f, 0f, targetRotation));
		foreach( GameObject g in objects) {
			g.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetRotation));
		}
	}
}
