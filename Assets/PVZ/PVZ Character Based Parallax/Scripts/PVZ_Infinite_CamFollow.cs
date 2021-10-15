//
//		Parallax Scrolling VZ
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 PVZ_Infinite_CamFollow
// - used to offset the background texture based on camera
//   movement
// - keeps character center based & wraps texture infinitely
//---------------------------------------------------------------/

using UnityEngine;
using System.Collections;

public class PVZ_Infinite_CamFollow : MonoBehaviour {

	public float scrollSpeed = 0.2F;	// speed to scroll the texture
	public bool isHorizontalScroll;		// determines if the parallax should be Horizontal
	public bool isVerticalScroll;		// determines if the parallax should be Vertical
	
	private GameObject thisGameObj;		// reference to the attached gameobject		
	public Camera theCam;				// reference to the camera the texture follows

	private Transform camTransform;		// ref to the camera's transform
	private Material textureMaterial;	// the material that is set to repeat itself

	
	void Awake ()
	{
		// set gameobject reference to local variable
		thisGameObj = gameObject;		
	}
	
	void Start () 
	{
		if(thisGameObj.GetComponent<Renderer>().material == null)
		{
			Debug.LogError("There is no texture attached. Please assign one in the inspector.");
		}
		else
		{
			// get the texture that will be wrapped
			textureMaterial = thisGameObj.GetComponent<Renderer>().material;
		}
		
		if(theCam == null)
		{
			Debug.LogError("There is no camera attached. Please assign one in the inspector.");
		}
		else 
		{
			//get the reference to the cameras transform
			camTransform = theCam.transform;			
		}

	}
	
	void FixedUpdate () 
	{
		if(isHorizontalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (camTransform.localPosition * scrollSpeed); 
			//set the offset in the texture's material
			textureMaterial.SetTextureOffset("_MainTex", theOffset);
		}
		
		
		if(isVerticalScroll)
		{
			//get the cam's pos & use it to determine offset
			Vector3 theOffset = (camTransform.localPosition * scrollSpeed); 
			//set the offset in the texture's material
			textureMaterial.SetTextureOffset("_MainTex", theOffset);
		}
	}
}

//
//
