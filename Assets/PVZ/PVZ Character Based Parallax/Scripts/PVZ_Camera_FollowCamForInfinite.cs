//
//		Parallax Scrolling VZ 
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 Camera_FollowCamForInfinite
// - used to follow the character & other camera 
// - keeps character in center
// - used in camera following with infinite texture wrapping
//---------------------------------------------------------------/

using UnityEngine;
using System.Collections;

public class PVZ_Camera_FollowCamForInfinite : MonoBehaviour {

	public Transform target;		//the target the camera follows
	private Transform thisTrans;	//reference to the cameras transform
	public float smoothing = 0.3f;	//how fast the camera changes position
	private Vector2 velocity;		//velocity of the camera

	
	
	void Awake ()
	{
		//set this transform to a local variable
		thisTrans = transform;	
	}
	
	void LateUpdate () 
	{
		//Get the current camera position
		Vector3 temp = thisTrans.position;

		//Change the camera position gradually to the target position over time
		temp.x = Mathf.SmoothDamp( thisTrans.position.x, target.position.x, ref velocity.x, smoothing);
		
		temp.y = Mathf.SmoothDamp( thisTrans.position.y,target.position.y, ref velocity.y, smoothing);

		//set the new position to the camera
		thisTrans.position = temp;
		
	}
}
