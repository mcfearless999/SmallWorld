using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieTracker : MonoBehaviour {

	public int bigCookies;
	public int smallCookies;
	public int scale;

	public GameObject bigCookiePrefab;
	public GameObject smallCookiePrefab;

	public UnityEngine.UI.Text text;
	SimplePlatformController spc;

	public float blowBackForce = 5000f;

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
				text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
				scale++;
				Camera.main.orthographicSize += 4;
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
				text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
				scale--;
				Camera.main.orthographicSize -= 4;
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
		if(c.gameObject.tag == "Breakable") {
			c.gameObject.GetComponentInChildren<Breakable>().Break(scale);
		}
		text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);

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
				//Debug.Log("I killed monster!");

				SpawnCookies(c.gameObject.transform.position);
				EnemyBehavior eb = c.GetComponentInChildren<EnemyBehavior>();
				eb.KillMe();
			} else {
				//Debug.Log("I got killed!");
				Vector2 f;
				if(spc.facingRight) {
					f = new Vector2(-blowBackForce, 100f);
				} else {
					f = new Vector2(blowBackForce, 100f);
				}
				if(bigCookies > 1 && smallCookies > 1) {
					bigCookies--;
					smallCookies--;
					SpawnCookies(transform.position);
				}
				GetComponentInChildren<Rigidbody2D>().AddForce(f);
			}
			
		}
		if(c.tag == "NextLevel") {
			UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex + 1);  	// Loads next scene in the index
																						// Last scene has to not have a NextLevel
		}
		if(c.tag == "PrevLevel") {
			UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
			int nextLevel = scene.buildIndex; // Used to scene.buildIndex - 1 but it is too frustrating to be sent back to the prev level
			if(nextLevel < 0) {
				nextLevel = 0;	// Level -1 is only for Mario
			}
			UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);  	// Loads next scene in the index
																						// Last scene has to not have a NextLevel
		}
		if(c.tag == "Moon") {
			gameObject.GetComponentInChildren<Rigidbody2D>().isKinematic = true;
			gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
			c.GetComponentInChildren<MoonScript>().Activate();
			Camera.main.transform.SetParent(c.transform);
			Vector3 v= c.transform.position;
			v.z -= 6;
			Camera.main.transform.position = v;
			gameObject.SetActive(false);

		}


		text.text = string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies);
		//Debug.Log(string.Format("Big Cookies: {0} \nSmall Cookies: {1}", bigCookies, smallCookies));
	}
	void SpawnCookies(Vector2 p) {
		float force = 30f;
		float offset = 5f;

		GameObject g1 = Instantiate(bigCookiePrefab);
		GameObject g2 = Instantiate(smallCookiePrefab);

		g1.GetComponentInChildren<CircleCollider2D>().isTrigger = false;
		g2.GetComponentInChildren<CircleCollider2D>().isTrigger = false;

		Vector2 p1 = p; p1.x += offset;
		Vector2 p2 = p; p2.x -= offset;

		g1.transform.position = p;
		g2.transform.position = p;

		g1.AddComponent<Rigidbody2D>();
		g2.AddComponent<Rigidbody2D>();

		g1.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(force, force));
		g2.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(-force, force));
	}
}
