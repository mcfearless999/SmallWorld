using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public float scale = 1f;
	public bool facingLeft = true;
	public bool dead = false;
	public float resetTime;
	public float time;
	BoxCollider2D boxCollider;

	float speed = 10.0f;
	float origX;
	float origY;
	Vector2 origScale;
	Animator anim;
	// Use this for initialization
	void Start ()
	{
		//Vector3 origPosition = transform.position;
		origX = transform.position.x;
		origY = transform.position.y;
		origScale = transform.localScale;
		boxCollider = GetComponentInChildren<BoxCollider2D>();
		anim = GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update ()
	{
		if(!dead) {
			transform.Translate(speed*Time.deltaTime,0,0);
				
			if(Mathf.Abs(origX - transform.position.x) > 5.0f)
			{
				
				speed *= -1.0f; //change direction
				Flip(); 
			}
		} else {
			time = Time.time;
			if (Time.time > resetTime) {
				transform.localScale = origScale;
				dead = false;
				boxCollider.enabled = true;

				// Undo "squish to ground" position move
				Vector2 p = transform.position;
				p.y = p.y + (1.3f * scale);
				transform.position = p;
				anim.enabled = true;
				GetComponentInChildren<SpriteRenderer>().flipX = true;  // Not sure why this gets unset?
			}
		}
	}
	public void Flip() {
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	public void KillMe() {
		Vector2 s = transform.localScale;
		s.y /= 10f;
		transform.localScale = s;

		// If we don't move the creature down, it will be squished in mid air
		Vector2 p = transform.position;
		p.y = p.y + (-1.3f * scale);
		transform.position = p;

		resetTime = Time.time + 30f; 
		dead = true;
		boxCollider.enabled = false;
		anim.enabled = false;
	}

}
