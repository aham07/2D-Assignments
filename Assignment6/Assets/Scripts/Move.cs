using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	Rigidbody2D charRB;

	float hVelocity;
	[Range(0.01f, 10.0f)]
	public float hScale = .05f;

	float vVelocity;

	[Tooltip("The height of our jump!")]
	[Range(0.5f, 20f)]
	public float jumpVal = 1.0f;
	[Tooltip("The velocity of our second jump!")]
	[Range(0.5f, 20f)]
	public float secondJumpVal = 1.5f;

	public float interJumpTime = .25f;
	float jumpStartTime;
	public int jumps;

	[Tooltip("Let's us know when the character is on the ground")]
	public bool onGround;


	// Use this for initialization
	void Start () {
		charRB = gameObject.GetComponent<Rigidbody2D> ();
		jumps = 0;
	}
	
	// Update is called once per frame
	void Update () {
		getHorizontal ();
		getJump ();
		move ();

	}

	//get horizontal speed
	void getHorizontal(){
		hVelocity = Input.GetAxis ("Horizontal") * hScale * Time.deltaTime;
	}

	//move character
	void move(){
		charRB.transform.position = new Vector2 (hVelocity + charRB.transform.position.x, charRB.transform.position.y);
		charRB.velocity += (Vector2.up * vVelocity); 
	}

	//jump code
	void getJump(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (jumps == 1 && ((Time.time - jumpStartTime) > interJumpTime)) {
				vVelocity = jumpVal * secondJumpVal;
				jumps++;
			}
			if (jumps < 2) {
				vVelocity = jumpVal;
				jumpStartTime = Time.time;
				jumps++;
			}
		} else {
			vVelocity = 0;
		}
	}

	//on ground trigger
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.CompareTag ("ground")) {
			if (!onGround) {
				onGround = true;
			}
			jumps = 0;
		}
	}

	//off ground trigger
	void OnTriggerExit2D(Collider2D coll){
		if (coll.CompareTag ("ground")) {
			if (onGround) {
				onGround = false;
			}
		}
	}

}
