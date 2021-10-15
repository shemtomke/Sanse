//
//		Parallax Scrolling VZ 
//		v. 1.1 - Updated June 9, 2015
//---------------------------------------------------------------//
// 	 PVZ_TextureOffset
// - scrolls a texture in a specified axis & direction
//	 using UV texture offset
// - setup via a custom editor - Parallax_Editor
//---------------------------------------------------------------//


using UnityEngine;
using System.Collections;

 
[RequireComponent (typeof (Material))]
[RequireComponent (typeof (MeshRenderer))]
[RequireComponent (typeof (Texture))]
public class PVZ_TextureOffset : MonoBehaviour {

	private GameObject thisGameObj;		// reference to the attached gameobject
	private Material textureMaterial;	// the material that is set to repeat itself
	private float offset;				// used to determine how much texture wrap occurs
	//variables used with Parallax Custom Editor
	public float scrollSpeed = 0.2F;	// the speed at which the texture scrolls/wraps
	public bool isHorizontalScroll;		// determines if the parallax should be Horizontal
	public bool isVerticalScroll;		// determines if the parallax should be Vertical
	public bool leftToRight = true;		// determines if the scroll direction is left-to-right
	public bool topToBottom = true;		// determines if the scroll direction is right-to-left
	public float scaleWidth;	// width of the attached texture
	public float scaleHeight;	// height of the attached texture
 

	void Awake ()
	{
		// set gameobject reference to local variable
		thisGameObj = gameObject;

	}
	void Start () 
	{
		// get the texture that will be wrapped
		textureMaterial = thisGameObj.GetComponent<Renderer>().material;
	}
	

	void Update () 
	{

		// Determine the amount to offset and wrap the texture - forces the UV scrolling to always scroll in the 0-1 space
		// to prevent jitter that may occur due to floating point imprecision
		offset = Mathf.Repeat(Time.time * scrollSpeed, 1);

		//-- Set the offset of the texture to move in the desired direction & axis ------//

		// HORIZONTAL Axis & Left-To-Right Direction
		if (isHorizontalScroll && leftToRight)
		{ 
			textureMaterial.SetTextureOffset("_MainTex", new Vector2(-offset, 0f));
		}
		// HORIZONTAL Axis & Right-To-Left Direction
		else if (isHorizontalScroll && !leftToRight)
		{
			textureMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
		}

		// VERTICAL Axis & Top-To-Bottom Direction
		if (isVerticalScroll && topToBottom)
		{
			textureMaterial.SetTextureOffset("_MainTex", new Vector2(0f, offset));
		}
		// VERTICAL Axis & Bottom-To-Top Direction
		else if (isVerticalScroll && !topToBottom)
		{
			textureMaterial.SetTextureOffset("_MainTex", new Vector2(0f, -offset));
		}

	}

}


//
//
//
