using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	public int breakableSize = 1;
	public GameObject littleStick;  // Prefab

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Break(int playerSize) {
		if(playerSize >= breakableSize) {
			for(int i = 1; i < Random.Range(3, 7); i++) {
				GameObject g = Instantiate(littleStick);
				g.transform.position = transform.position;
				g.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1000, 1000), Random.Range(0, 1000)));
				g.GetComponentInChildren<Rigidbody2D>().AddTorque(Random.Range(-180, 180));
			}
			Destroy(gameObject);
		}
	}
}
