using UnityEngine;
using System.Collections;

public class Hax : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 v = this.transform.position;
		v.z = -2;
		this.transform.position = v;
	}
}
