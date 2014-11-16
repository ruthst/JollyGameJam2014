using UnityEngine;
using System.Collections;

public class IgnoreCollison : MonoBehaviour {

	public Transform other;
	// Use this for initialization
	void Start()
	{
		Physics2D.IgnoreCollision(other.collider2D, this.transform.collider2D);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
