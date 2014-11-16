using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {

	private bool sam = false;
	private bool biggie = false;
	private bool slink = false;

	void Update () 
	{
		if (biggie && slink && sam && Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel((Application.loadedLevel + 1)%2);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Sam") {
			sam = true;
		}
		if (other.tag == "Slink") {
			slink = true;
		}
		if (other.tag == "Biggie") {
			biggie = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Sam") {
			sam = false;
		}
		if (other.tag == "Slink") {
			slink = false;
		}
		if (other.tag == "Biggie") {
			biggie = false;
		}
	}
}
