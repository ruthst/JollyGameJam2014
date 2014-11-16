using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider){
		print ("Trigger Enter");
		if(collider.tag == "Sam") {
			print ("NEAR CONSOLE");
			collider.GetComponent<Sam>().nearConsole = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D collider){
		print ("Trigger Exit");
		if(collider.tag == "Sam") {
			print ("NOT NEAR CONSOLE");
			collider.GetComponent<Sam>().nearConsole = false;
		}
	}
}
