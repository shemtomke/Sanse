//
//		Parallax Scrolling VZ
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 Camera_FollowChar
// - used to follow the character whose parallax layers are
//   affected by
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;

public class Camera_FollowChar : MonoBehaviour {

	public Transform target;		//the target the camera follows
	private Transform thisTrans;	//reference to the cameras transform
	public float smoothing = 0.3f;	//how fast the camera changes position
	private Vector2 velocity;       //velocity of the camera
	public Vector2 offset;



	void Awake ()
	{
		//set this transform to a local variable
		thisTrans = transform;	
	}

	void FixedUpdate () 
	{
		transform.position = new Vector2(target.position.x + offset.x, target.position.y + offset.y);

		//Get the current camera position
		Vector3 temp = thisTrans.position;

		//Change the camera position gradually to the target position over time
		temp.x = Mathf.SmoothDamp( thisTrans.position.x, target.position.x, ref velocity.x, smoothing);

		temp.y = Mathf.SmoothDamp( /*thisTrans.position.y*/38,target.position.y, ref velocity.y, smoothing);

		//set the new position to the camera
		thisTrans.position = temp;

		

	}
}

