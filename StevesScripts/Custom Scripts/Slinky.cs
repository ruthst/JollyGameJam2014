using UnityEngine;
using System.Collections;

public class Slinky : MonoBehaviour
{

	public GameObject[] GroundDetectors;
	public Camera RenderingCamera;
	public Slinky friend;
	public BigController big;
	
	private GameObject grabbed;
	private Vector2 start;
	
	private float speed = 6f;
	private bool grounded;
	
	public bool left;
	public bool pickup;
	
	void Start ()
	{
		grabbed = null;
		grounded = false;
		pickup = false;
	}

	void FixedUpdate ()
	{
		bool wasGrounded = grounded;
		grounded = false;
		foreach(GameObject gd in GroundDetectors){
			if(Physics2D.Linecast(this.transform.position, gd.transform.position, 1 << LayerMask.NameToLayer ("Ground")))
				grounded = true;
		}
		if(!wasGrounded && grounded)
		{
			SoundEffectsHelper.Instance.MakeSlinkSound();
		}
		if (grounded && grabbed != this.gameObject)
				this.rigidbody2D.isKinematic = true;
		else
				this.rigidbody2D.isKinematic = false;
		
		
		if ((Input.GetMouseButtonDown(0) && left) || (Input.GetMouseButtonDown(1) && !left))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Began);
		}
		if ((Input.GetMouseButtonUp(0) && left) || (Input.GetMouseButtonUp(1) && !left))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Ended);
		}
		if ((Input.GetMouseButton(0) && left) || (Input.GetMouseButton(1) && !left))
		{
			this.CastTouchRay (Input.mousePosition, TouchPhase.Moved);
		}
		if (grabbed == friend.gameObject)
		{
			this.rigidbody2D.velocity = new Vector2();
			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		
		if (grabbed == this.gameObject && Input.GetMouseButtonDown (2) && !pickup) {
			if(Physics2D.Linecast(this.transform.position, GroundDetectors[0].transform.position, 1 << LayerMask.NameToLayer ("Sam"))){
				print ("Bind");
				big.setDestination(this.gameObject);
				pickup = true;
			}
		}
		if (Input.GetMouseButtonUp(2) && pickup){
			print ("unbind");
			big.setDestination(null);
			pickup = false;
			
		}
	}
	
	void CastTouchRay (Vector2 position, TouchPhase phase)
	{
		Vector2 worldPoint = RenderingCamera.ScreenToWorldPoint(position);
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0);
		bool didHit = null != hit.collider;
		if (didHit)
		{
			GameObject recipient = hit.transform.gameObject;
			if(recipient == this.gameObject){		
				if(phase == TouchPhase.Began && friend.grounded){
					friend.rigidbody2D.isKinematic = true;
					grabbed = this.gameObject;
					this.rigidbody2D.gravityScale = 0;
				}
			}
			
		}			
		if(grabbed == this.gameObject){
			if(phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
			{
				Vector2 difference = (worldPoint - this.rigidbody2D.position);

				if(friend.grounded)
					this.rigidbody2D.velocity = (difference * speed);
				else
				{
					grabbed = null;
					this.rigidbody2D.gravityScale = 2;
					this.rigidbody2D.velocity = new Vector2();
				}
			}
			if(phase == TouchPhase.Ended)
			{
				friend.rigidbody2D.isKinematic = false;
				grabbed = null;
				this.rigidbody2D.gravityScale = 2;
			}
		}
	}
}
