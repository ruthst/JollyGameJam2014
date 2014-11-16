using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public float length;
	public float charSize;

	private Vector3 start;
	// Use this for initialization
	void Start () {
		length = 0;
		start = this.transform.localPosition;
	}

	void Update() {
		//		print (start + new Vector3 (length * charSize, 0, 0));
		this.transform.localPosition = start + new Vector3 (length, 0, -3);
	}
}
