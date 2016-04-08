
using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	Rigidbody2D charRB;
	public bool isHover;

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

	//this helps with character direction
	bool facingRight = true;

	Animator animator;


	// Use this for initialization
	void Start () {
		charRB = gameObject.GetComponent<Rigidbody2D> ();
		jumps = 0;
		animator = GetComponent<Animator>();
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
		if (hVelocity > 0 && !facingRight) {
			flip ();
		}
		else if(hVelocity < 0 && facingRight){
			flip ();
		}

		animator.SetBool("onGround", onGround);
		animator.SetFloat ("hSpeed", Mathf.Abs(hVelocity));
		charRB.transform.position = new Vector2 (charRB.transform.position.x + hVelocity, charRB.transform.position.y);
		charRB.velocity += (Vector2.up * vVelocity);

		animator.SetFloat ("vVelocity", charRB.velocity.y);
	}

	//jump code
	void getJump(){
		//for animal and human
		if (Input.GetKeyDown (KeyCode.Space) && isHover == false) {
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

		//for android
		if (Input.GetKeyDown (KeyCode.Space) && isHover == true) {			
				vVelocity = jumpVal;
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

	void flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1.0f;
		transform.localScale = theScale;
	}

}
