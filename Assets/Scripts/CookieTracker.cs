using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTracker : MonoBehaviour {

	public int bigCookies;
	public int smallCookies;

	public GameObject bigCookiePrefab;
	public GameObject smallCookiePrefab;

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
	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.tag == "BigCookie") {
			bigCookies++;
			Destroy(c.gameObject);
		}
		if(c.gameObject.tag == "SmallCookie") {
			smallCookies++;
			Destroy(c.gameObject);
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
			if(spc.killMonster) {
				Debug.Log("I killed monster!");
				GameObject g1 = Instantiate(bigCookiePrefab);
				GameObject g2 = Instantiate(smallCookiePrefab);

				g1.GetComponentInChildren<CircleCollider2D>().isTrigger = false;
				g2.GetComponentInChildren<CircleCollider2D>().isTrigger = false;

				g1.transform.position = c.transform.position;
				g2.transform.position = c.transform.position;

				g1.AddComponent<Rigidbody2D>();
				g2.AddComponent<Rigidbody2D>();

				g1.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(10f, 10f));
				g2.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-10f, 10f));

				Destroy(c.gameObject);
			} else {
				Debug.Log("I got killed!");
				// Do kill thing here
			}
			
		}
		text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
		Debug.Log(string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies));
	}
}
