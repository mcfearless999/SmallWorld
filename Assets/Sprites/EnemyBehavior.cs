using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour {

	public bool facingLeft = true;
	float speed = 10.0f;
	float origX;

	// Use this for initialization
	void Start ()
	{
		//Vector3 origPosition = transform.position;
		origX = transform.position.x;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(speed*Time.deltaTime,0,0);

		if(Mathf.Abs(origX - transform.position.x) > 4.0f)
		{
			
			speed *= -1.0f; //change direction
			Flip(); 
		}
	}
	public void Flip() {
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
