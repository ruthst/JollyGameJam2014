using UnityEngine;
using System.Collections;

public class BigController : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing
	
	public float moveDistance = 0.4f;			// Amount of distance player is moved left or right
	public float waitTime = 0.2f;				// Wait time between movements

	private bool right = false;
	private bool left = false;
	private GameObject target;
	
	void Update()
	{
		int h = (int)Input.GetAxis("Horizontal");
		// left stick is centered
		if (h == 0)
		{
			right = false;
			left = false;
		}
		// left stick is right
		if (h > 0) 
		{
			if (Input.GetButtonDown("right") && !right) 
			{
				right = !right;
				StartCoroutine(Move(moveDistance, true, false));
				Debug.Log("right pressed");
			}
			
			if (Input.GetButtonDown("left") && right) {
				left = !left;
				StartCoroutine(Move(moveDistance, false, false));
				Debug.Log("left pressed");
			}
		}
		// left stick is left
		if (h < 0) 
		{
			if (Input.GetButtonDown("left") && !left) {
				left = !left;
				StartCoroutine(Move(-moveDistance, false, true));
				Debug.Log("left pressed");
			}
			
			if (Input.GetButtonDown("right") && left) {
				right = !right;
				StartCoroutine(Move(-moveDistance, false, false));
				Debug.Log("right pressed");
			}
		}
		// If the input is moving the player right and the player is facing left...
		//if(h > 0 && !facingRight) {
			
			// ... flip the player.
		//Flip();
		//}
		// Otherwise if the input is moving the player left and the player is facing right...
		//else if(h < 0 && facingRight)
			// ... flip the player.
			//Flip();
		if (target) {
			Vector2 position = target.rigidbody2D.position;
			position.y -= 1.8f;
			this.rigidbody2D.position = position;
		}

	
	}	

	public void setDestination(GameObject other){
		target = other;
	}

	IEnumerator Move(float moveDistance, bool one, bool two) {
		transform.position = new Vector2(transform.position.x + moveDistance, transform.position.y);
		yield return new WaitForSeconds(waitTime);
		right = one;
		left = two;
	}
	
	//void Flip ()
	//{
		// Switch the way the player is labelled as facing.
		//facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		//Vector3 theScale = transform.localScale;
		//theScale.x *= -1;
		//transform.localScale = theScale;
	//}
}
