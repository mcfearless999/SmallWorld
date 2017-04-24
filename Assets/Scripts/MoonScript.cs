using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour {

	public GameObject princess;
	public GameObject prince;
	public UnityEngine.UI.Button button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Activate() {
		princess.SetActive(true);
		prince.SetActive(true);
		button.gameObject.SetActive(true);
		button.enabled = true;
	}
	public void Level1() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
	}
}
