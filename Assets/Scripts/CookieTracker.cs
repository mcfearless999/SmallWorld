using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTracker : MonoBehaviour {

	public int bigCookies;
	public int smallCookies;

	public UnityEngine.UI.Text text;
	SimplePlatformController spc;

	// Use this for initialization
	void Start () {
		spc = GetComponentInChildren<SimplePlatformController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			// Embiggen
			if(bigCookies > 0) {
				bigCookies--;
				Vector3 newScale = transform.localScale;
				newScale.x *= 2;
				newScale.y *= 2;
				transform.localScale = newScale;
				spc.jumpForce *=2;
			}
		}
		if(Input.GetButtonDown("Fire2")) {
			// Emsmallen
			if(smallCookies > 0) {
				smallCookies--;
				Vector3 newScale = transform.localScale;
				newScale.x /= 2;
				newScale.y /= 2;
				transform.localScale = newScale;
				spc.jumpForce /=2;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if(c.tag == "BigCookie") {
			bigCookies++;
			Destroy(c.gameObject);
		}
		if(c.tag == "SmallCookie") {
			smallCookies++;
			Destroy(c.gameObject);
		}
		if(c.tag == "TriggerText") {
			if(bigCookies + smallCookies > 0) {
				c.GetComponentInChildren<MeshRenderer>().enabled = true;
			}
		}
		if(c.tag == "Monster") {
			
		}
		text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
		Debug.Log(string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies));
	}
}
