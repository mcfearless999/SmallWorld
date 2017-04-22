using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	public bool facingRight = true;
	public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	public bool grounded = false;
	Animator anim;
	Rigidbody2D rb;
	public Vector2 gravity;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");
		if(!grounded) { h = 0f; }

		anim.SetFloat("Speed", Mathf.Abs(h));

		if(h*rb.velocity.x < maxSpeed) {
			rb.AddForce(Vector2.right * h * moveForce);
		}

		if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2 (Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}
		if(h > 0 && !facingRight) { Flip(); }
		if(h < 0 &&  facingRight) { Flip(); }

		if(jump) {
			anim.SetTrigger("Jump");
			rb.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
//		Physics2D.gravity = (-1 * transform.position.normalized);
//		gravity = Physics2D.gravity;
//		if(grounded) {
//			transform.Rotate(Vector3.up, Mathf.Rad2Deg * ( Mathf.Atan2(transform.position.x, transform.position.y)));
//		}
		//transform.rotation =  Quaternion.Euler( new Vector3(0, 0, -1 * (transform.position.y / transform.position.x)));
	}
	 
	public void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
