using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTracker : MonoBehaviour {

	public int bigCookies;
	public int smallCookies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c) {
		Debug.Log(c.tag);
		if(c.tag == "BigCookie") {
			bigCookies++;
			Destroy(c.gameObject);
		}
	}
}
