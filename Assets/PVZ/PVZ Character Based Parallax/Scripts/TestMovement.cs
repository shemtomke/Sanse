//
//		Parallax Scrolling VZ
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 TestMovement
// - used to test sprite character movement with a parallax
//   environment
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;

public class TestMovement : MonoBehaviour {


	private GameObject thisGameObj;			// reference to the attached gameobject
	public float moveForce = 365f;			// Amount of force added to move the player left and right
	public float maxSpeed = 5f;				// max speed the player can travel in the x axis
	public bool jump = false;				// determine whether player can jump or not
	private Transform groundCheck;			// A position marking where to check if the player is grounded
	private bool grounded = false;			// Whether or not the player is grounded
	public float jumpForce = 1000f;			// Vertical force to simulate jumping
	public bool isHorizontalMovement = true;		// Should the character be allowed horizontal movement			
	private bool isVerticalMovement;			// Should the character be allowed vertical movement	

	void Awake ()
	{
		// set gameobject reference to local variable
		thisGameObj = gameObject;
		// set reference to the groundcheck object
		groundCheck = transform.Find("groundCheck");
	}
	void Start()
	{
		isVerticalMovement = !isHorizontalMovement;
	}

	void Update () 
	{
		if(isHorizontalMovement)
		{
			// check if the player is grounded
			grounded = Physics2D.Linecast(thisGameObj.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  
			
			// If jump button is press & player is grounded then set jump
			if(Input.GetButtonDown("Jump") && grounded)
				jump = true;
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if(isHorizontalMovement)
		{
			//check if the character and the input is less than the max speed
			if (h * thisGameObj.GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			{
				// Add horizontal force to object
				thisGameObj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);		
			}

			// Make sure the objects velocity stays under the max speed
			if(Mathf.Abs(thisGameObj.GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			{
				thisGameObj.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(thisGameObj.GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, thisGameObj.GetComponent<Rigidbody2D>().velocity.y);		
			} 
		}

		if(isVerticalMovement)
		{
			//check if the character and the Y input is less than the max speed
			if (v * thisGameObj.GetComponent<Rigidbody2D>().velocity.y < maxSpeed)
			{
				// Add horizontal force to object
				thisGameObj.GetComponent<Rigidbody2D>().AddForce(Vector2.up * v * moveForce);		
			}

			// Make sure the objects velocity stays under the max speed
			if(Mathf.Abs(thisGameObj.GetComponent<Rigidbody2D>().velocity.y) > maxSpeed)
			{
				Vector2 tempVel = thisGameObj.GetComponent<Rigidbody2D>().velocity;
				tempVel.y = maxSpeed * v;
				thisGameObj.GetComponent<Rigidbody2D>().velocity = tempVel;
			}
		}
		// Enter if the jump condition is set in update
		if(jump)
		{		
			// Add a vertical force to player
			thisGameObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied
			jump = false;
		}
	}
}
