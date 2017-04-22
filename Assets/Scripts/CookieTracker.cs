using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTracker : MonoBehaviour {

	public int bigCookies;
	public int smallCookies;

	public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
		text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
	}
}
