//
//		Parallax Scrolling VZ 
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 PVZ_NotInfinite_CameraFollow
// - scrolls attached texture based on the camera movement
// - textures are not infinite (don't wrap)
// - character will stay in center of camera's focus
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;

public class PVZ_NotInfinite_CameraFollow : MonoBehaviour {

	public float scrollSpeed = 0.2F;	// speed to scroll the texture
	public bool isHorizontalScroll;		// determines if the parallax should be Horizontal
	public bool isVerticalScroll;		// determines if the parallax should be Vertical
	
	private GameObject thisGameObj;		// reference to the attached gameobject	
	public Transform theCharacter;		// reference to the the character to follow
	private Vector3 previousCamPos;		// keeps the previous position of the camera

	public Camera theCam;				// reference to the camera the texture follows
	private float camPosition;			// keeps the current position of the camera
	public float smoothing = 1.0f;		// smoothing of the textures changing position
	

	void Awake ()
	{
		// set gameobject reference to local variable
		thisGameObj = gameObject;		
	}

	void Start () 
	{
		if(theCharacter == null)
		{
			Debug.LogError("There is no Character attached. Please assign one in the inspector.");
		}

		if(theCam == null)
		{
			Debug.LogError("There is no camera attached. Please assign one in the inspector.");
		}
		else 
		{
			//set the previous cam position to its starting position
			previousCamPos = theCam.transform.position;

		}
	}

	void FixedUpdate () 
	{
		if(isHorizontalScroll)
		{
		// Get the textures position
		Vector3 tempPos = thisGameObj.transform.position;

		//tempPos.x = (theCam.transform.position.x - camPosition) / scrollSpeed;

		// Set the amount of parallax based on the camera's position differences divided by the scrolling speed
		float parallax = (previousCamPos.x - theCam.transform.position.x) / scrollSpeed;
		//thisGameObj.transform.position = tempPos;

		// Get the texture's X position with the parallax and set it to the target position's x value
		float layerTargetPosX = thisGameObj.transform.position.x + parallax;
		// get the textures position and add in the parallax target position X
		Vector3 layerTargetPos = new Vector3(layerTargetPosX,tempPos.y,tempPos.z);

		// Set the textures new position - based on the previous position, target positon
		thisGameObj.transform.position = Vector3.Lerp(thisGameObj.transform.position,layerTargetPos, smoothing * Time.deltaTime);
		// Assign the cameras current position to its new previous
		previousCamPos = theCam.transform.position;
		}


		if(isVerticalScroll)
		{
			// Get the textures position
			Vector3 tempPos = thisGameObj.transform.position;
			
			//tempPos.x = (theCam.transform.position.x - camPosition) / scrollSpeed;
			
			// Set the amount of parallax based on the camera's position differences divided by the scrolling speed
			float parallax = (previousCamPos.y - theCam.transform.position.y) / scrollSpeed;
			//thisGameObj.transform.position = tempPos;

			// Get the texture's X position with the parallax and set it to the target position's x value
			float layerTargetPosY = thisGameObj.transform.position.y + parallax;
			// get the textures position and add in the parallax target position X
			Vector3 layerTargetPos = new Vector3(tempPos.x,layerTargetPosY,tempPos.z);
			
			// Set the textures new position - based on the previous position, target positon
			thisGameObj.transform.position = Vector3.Lerp(thisGameObj.transform.position,layerTargetPos, smoothing * Time.deltaTime);
			// Assign the cameras current position to its new previous
			previousCamPos = theCam.transform.position;
		}
	}
}
