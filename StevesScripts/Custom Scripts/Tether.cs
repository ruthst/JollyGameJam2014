using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour {

	public Slinky h1;
	public Slinky h2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 p1 = h1.rigidbody2D.position;
		Vector2 p2 = h2.rigidbody2D.position;

		this.transform.position = (p1 + p2) / 2;

		float length = (p2 - p1).magnitude;
		Vector3 scale = this.transform.localScale;
		scale.x = length;
		this.transform.localScale = scale;

		float slope = (p2.y - p1.y)/(p2.x - p1.x); 
		float angle = Mathf.Atan (slope) * Mathf.Rad2Deg;
		this.transform.localEulerAngles = new Vector3 (0, 0, angle);
	}
}
