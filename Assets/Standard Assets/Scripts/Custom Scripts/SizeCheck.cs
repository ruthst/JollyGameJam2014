using UnityEngine;
using System.Collections;

public class SizeCheck : MonoBehaviour {
	Vector3 scale;
	// Use this for initialization
	void Start () {
		scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = scale;
	}
}
