using UnityEngine;
using System.Collections;
using Jolly;

public class Sam : MonoBehaviour
{
	public int sideSpeed;
	public int JumpForce;
	public GameObject GroundDetector;
	public Camera RenderingCamera;
	public BigArmsController BigBro;
	public TextMesh Text;
	public Cursor cursor;
	public GameObject objectToDisable;

	public GameObject bubble;
	public Sprite normal;
	public Sprite taunt1;
	public Sprite taunt2;
	public Sprite taunt3;
	public Sprite taunt4;
	public Sprite taunt5;

	private string command;
	public bool nearConsole = false;
	public float hackSeconds = 7f;

	private bool jump;
	private bool left;
	private bool right;
	private bool stop;
	private int tauntCounter = 4;

	void Start ()
	{
		left = false;
		right = false;
		command = "";
	}
	
	void Update ()
	{
		//check if grounded for jump handling
		bool grounded = Physics2D.Linecast(this.transform.position, this.GroundDetector.transform.position, 1 << LayerMask.NameToLayer ("Ground"));


		//make sure grounded on feet


		//taunt randomization
		tauntCounter = ++tauntCounter % 5;

		//get input
		foreach (char c in Input.inputString) {
			if (c == "\b"[0]){
				if (command.Length != 0)
					command = command.Substring(0, command.Length - 1);
			}
			
			else if (c == "\n"[0] || c == "\r"[0]){
				this.Command(command);
				command = "";
			}
			else{
				cursor.renderer.enabled = true;
				bubble.GetComponent<SpriteRenderer> ().sprite = normal;
				command += c;
			}
		}

		//update cursor position
		Bounds bounds = Text.renderer.bounds;
		cursor.length = bounds.extents.x * 2;
		Text.text = command;
		if (jump && !grounded)
			jump = false;
	}

	//movement handling
	void FixedUpdate ()
	{
		if (jump)
		{
			SoundEffectsHelper.Instance.MakeJumpSound();
			this.rigidbody2D.AddForce (Vector2.up * JumpForce);
			this.jump = false;
		}
		Vector2 velocity = this.rigidbody2D.velocity;
		if (right) {
			//print ("right");
			velocity.x = sideSpeed;
		}
		else if (left){
			//print ("left");
			velocity.x = -1 * sideSpeed;
		}
		else if (stop){
			velocity.x = 0;
			stop = false;
		}
		this.rigidbody2D.velocity = velocity;

	}


	//flip if side frames are created
//	void Flip ()
//	{
//		this.FacingRight = !this.FacingRigh
//		this.transform.localScale = this.transform.localScale.SetX(this.FacingRight ? 1.0f : -1.0f);
//	}


	void taunt(){
		cursor.renderer.enabled=false;

		if (tauntCounter == 0)
			bubble.GetComponent<SpriteRenderer> ().sprite = taunt1;
		else if (tauntCounter == 1)
			bubble.GetComponent<SpriteRenderer> ().sprite = taunt2;
		else if (tauntCounter == 2)
			bubble.GetComponent<SpriteRenderer> ().sprite = taunt3;
		else if (tauntCounter == 3)
			bubble.GetComponent<SpriteRenderer> ().sprite = taunt4;
		else if (tauntCounter == 4)
			bubble.GetComponent<SpriteRenderer> ().sprite = taunt5;


	}

	void Command(string text)
	{
		if (text == "right") {
				right = true;
				left = false;
		} else if (text == "left") {
				left = true;
				right = false;
		} else if (text == "jump") {
				jump = true;
		} else if (text == "jump right") {
				jump = true;
				right = true;
				left = false;
		} else if (text == "jump left") {
				jump = true;
				left = true;
				right = false;
		} else if (text == "stop") {
				right = false;
				left = false;
			stop = true;
		} else if (text == "taunt") {
				taunt ();
				BigBro.rager ();
		} else if (text == "reset") {
				Application.LoadLevel(Application.loadedLevel);
		} else if (text == "quit") {
				Application.Quit();
		} else if (text == "hack") {
			print ("hack");
			print (nearConsole);		
			if (nearConsole) {
				SoundEffectsHelper.Instance.MakeHackSound();
				StartCoroutine(hack ());
			
			}
		} else if (text == "taunt") {
				taunt ();
				BigBro.rager ();
		}

				

	}

	void OnTriggerEnter(Collider collider){
		print ("Trigger Enter");
		if(collider.gameObject.tag == "Terminal") {
			print("NEAR CONSOLE");
			nearConsole = true;
		}
	}

	IEnumerator hack() {
	
		objectToDisable.SetActive(false);
		SoundEffectsHelper.Instance.MakeLaserSound();
		yield return new WaitForSeconds(hackSeconds);
		objectToDisable.SetActive(true);

	}
	

}	