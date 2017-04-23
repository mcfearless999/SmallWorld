using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour {

	float deathTime;

	// Use this for initialization
	void Start () {
		deathTime = Time.time + 60f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > deathTime) {
			Destroy(gameObject);
		}
	}
}
