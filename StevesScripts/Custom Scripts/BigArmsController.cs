using UnityEngine;
using System.Collections;

public class BigArmsController : MonoBehaviour {
	
	public float force = 364f;
	//	public GameObject hands;
	public float rageDuration = 4f;
	public bool raging = false;
	private bool grab = false;
	public float wait = 0.02f;
	public GameObject regHead;
	public GameObject rageHead;


	private Vector2 centerHands; 

	void Start()
	{
		Physics2D.IgnoreCollision(transform.parent.transform.collider2D, this.transform.collider2D);
	}

	void Update() 
	{
		grab = Input.GetButton("Grab");
		
		
		// start rage mode with a button
		//if (Input.GetKeyDown(KeyCode.A))
			//StartCoroutine(Rage());
		
		// while raging send random values to the arms
		if (raging) 
		{
			grab = true;
			print (raging);
		}
		
		// must hold grab to initiate rotating
		if (grab) 
		{
//			Debug.Log ("Grab held");
			
			float x = Input.GetAxis("Rotate Horz");
			float y = -1 * Input.GetAxis("Rotate Vert");
			
//			Debug.Log("x = " + x + ", y = " + y);
			
			if (Mathf.Abs(x) > .35 || Mathf.Abs(y) > .35){
				float angle = Mathf.Atan(y/x) * Mathf.Rad2Deg;
				
				if(x > 0)
					angle += 180;
				this.transform.localEulerAngles = new Vector3(0,0,angle - 90);
			}
			
		}
		else 
		{
			float x = Input.GetAxis("Rotate Horz");
			float y = -1 * Input.GetAxis("Rotate Vert");
			
			foreach (Transform child in transform)
			{
				if (child.tag == "Sam") {
					child.gameObject.rigidbody2D.isKinematic = false;
					child.parent = null;
					child.gameObject.rigidbody2D.AddForce((new Vector2(x, y)) * force);
					child.gameObject.transform.rotation = Quaternion.identity;
					SoundEffectsHelper.Instance.MakeGruntSound();
				}
				
			}
			// snap arms back to sides
			transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			
		}


	}


	
	void OnCollisionEnter2D(Collision2D other)
	{
//		Debug.Log("collision");
		
		// for rage mode
//		if (raging && other.gameObject.tag == "Wall") 
//		{
//
//			other.gameObject.rigidbody2D.AddForce(new Vector2(Random.Range(1f, 20f), Random.Range(1f, 20f)));
//		}
		
		if (Input.GetButton("Grab") && other.gameObject.tag == "Sam")
		{
			// other is now a child of this
			foreach (Transform child in transform)
			{
				if (child.name == "Center_Hands")
					//					Debug.Log(child.position);
					centerHands = child.position;
			}
			//			Debug.Log(centerHands);
			other.gameObject.transform.position = centerHands;
			other.gameObject.transform.parent = this.transform;
			other.gameObject.rigidbody2D.isKinematic = true;
		}
	}
	
	public void rager(){
		StartCoroutine (Rage());
	}
	
	IEnumerator Rage()
	{
		SoundEffectsHelper.Instance.MakeGruntSound();
		raging = true;
		rageHead.renderer.enabled = true;
		regHead.renderer.enabled = false;
		StartCoroutine(CrazyArms());
		yield return new WaitForSeconds(rageDuration);
		raging = false;
		rageHead.renderer.enabled = false;
		regHead.renderer.enabled = true;
		transform.localEulerAngles = new Vector3(0f, 0f, 0f);

	}

	IEnumerator CrazyArms()
	{
		int counter = 0;
		while (raging) {
			if (counter % 20 == 1) {
				SoundEffectsHelper.Instance.MakeGruntSound();
			}
			this.transform.localEulerAngles = new Vector3(0, 0 ,Random.Range(0, 360));
			counter++;
			yield return new WaitForSeconds(wait);
		}
	}
	
}
