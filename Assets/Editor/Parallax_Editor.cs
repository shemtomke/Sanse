//
//		Parallax Scrolling VZ 
//---------------------------------------------------------------//
// 	 Parallax_Editor
// - uses PVZ_TextureOffset
//   to set the scrolling axis, direction, and speed for offset
//   scrolling. Also finds a textures dimensions, aspectratio
//   and the scale needed to set a pixel perfect x & y scale in
//   the inspector
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PVZ_TextureOffset))]
[CanEditMultipleObjects]
public class Parallax_Editor : Editor  
{
//	bool isHorz;	
//	bool isVert;
//	GameObject obj;
	SerializedProperty isHorizScroll;	// Determine if the scroll should be horizontal
	SerializedProperty isVertScroll;	// Determine if the scroll should be vertical
	SerializedProperty lToR;			// Determine if the scroll should be Left to Right direction
	SerializedProperty tToB;			// Determine if the scroll should be Top to Bottom direction
	SerializedProperty speed;			// the speed at which the texture scrolls
	
	float heightIs;					// Used to find the textures height
	float widthIs;					// Used to find the textures width
	SerializedProperty scaleX;		// Used to find the Scale X based on Scale Y & aspectratio
	SerializedProperty scaleY;		// Used to find the Scale Y based on Scale X & aspectratio
	float aspectRatio;				// the ratio of the textures dimensions

	bool noMaterial = false;		//determines if a texture is attached to object - to prevent errors
	public PVZ_TextureOffset myTarget;		//object of Parallax_TextureOffset to get variables
	

	public override void OnInspectorGUI()
	{
		// Updates the serialized objects
		serializedObject.Update ();

		//if there is no material continue checking until user attaches a material
		if(noMaterial)
		{
			CheckForMaterial();
		}

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("//////////    PARALLAX VZ    //////////");

		EditorGUILayout.LabelField("-----------------------------------------------");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("SCROLLING AXIS, DIRECTION, & SPEED");

		// Determine if the Horizontal field should be disabled or not
		GUI.enabled = !isVertScroll.boolValue;

		EditorGUILayout.PropertyField(isHorizScroll, new GUIContent ("Horizontal Scroll"));

		// If Horizontal is unchecked then disable the LeftoRight option
		if(isHorizScroll.boolValue == false)
		{
			GUI.enabled = false;
		}
		EditorGUILayout.PropertyField(lToR, new GUIContent ("      Left to Right ->"));

		// Determine if the Vertical field should be disabled or not
		GUI.enabled = !isHorizScroll.boolValue;
		EditorGUILayout.PropertyField(isVertScroll, new GUIContent ("Vertical Scroll"));

		// If Vertical is unchecked then disable the TopToBottom option
		if(isVertScroll.boolValue == false)
		{
			GUI.enabled = false;
		}
		EditorGUILayout.PropertyField(tToB, new GUIContent ("      Top to Bottom"));
		EditorGUILayout.Space();
		GUI.enabled = true;
		EditorGUILayout.PropertyField(speed, new GUIContent ("Scroll Speed"));

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("-----------------------------------------------");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("ATTACHED TEXTURE'S DIMENSIONS");
		// Show the textures dimensions
		EditorGUILayout.LabelField("Width:", widthIs.ToString()+ " px");
		EditorGUILayout.LabelField("Height:", heightIs.ToString()+ " px");

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("-----------------------------------------------");
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("FIND PIXEL PERFECT SCALE - CHANGE ONE");
		EditorGUILayout.LabelField("VALUE TO GET THE OTHER");
		//find the aspect ratio to determine scaleX, scaleY based on user input
		aspectRatio = widthIs/heightIs;
		EditorGUILayout.PropertyField(scaleX, new GUIContent ("Scale X"));
		scaleY.floatValue = scaleX.floatValue/aspectRatio;
		EditorGUILayout.PropertyField(scaleY, new GUIContent ("Scale Y"));
		scaleX.floatValue = scaleY.floatValue * aspectRatio;

		// Apply property modifications
		serializedObject.ApplyModifiedProperties ();

	}

	void OnEnable()
	{
		// Set the target to reference variable from PVZ_TextureOffset
		myTarget = (PVZ_TextureOffset)target;

		// Make sure the target is not null and there is a shared Material
		if (myTarget && myTarget.gameObject && myTarget.GetComponent<MeshRenderer>().sharedMaterial == null)
		{
			Debug.LogError("There is no MeshRenderer and/or Material attached to " + myTarget.gameObject.name);
			noMaterial = true;
		}
		else{	//if target is there, make sure there is a maintexture attached
			if(myTarget.GetComponent<Renderer>().sharedMaterial.mainTexture == null)
			{
				Debug.LogError("There is no MainTexture attached to " + myTarget.gameObject.name + ". Add a Material to the MeshRenderer.");
				//CheckForMaterial();
				noMaterial = true;
			}
			else{
				// If there is a texture attached then set local height and width to display
				heightIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.height;
				widthIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.width;
			}
		}

		// Get variables from PVZ_TextureOffset
		isHorizScroll = serializedObject.FindProperty ("isHorizontalScroll");
		isVertScroll = serializedObject.FindProperty ("isVerticalScroll");
		lToR = serializedObject.FindProperty ("leftToRight");
		tToB = serializedObject.FindProperty ("topToBottom");
		speed = serializedObject.FindProperty ("scrollSpeed");
		scaleX = serializedObject.FindProperty ("scaleWidth");
		scaleY = serializedObject.FindProperty ("scaleHeight");

	}

	public void CheckForMaterial()
	{
		if(myTarget.GetComponent<Renderer>().sharedMaterial.mainTexture == null)
		{
			//Debug.LogError("There is no MainTexture attached to " + myTarget.gameObject.name + ". Add a Material to the MeshRenderer.");
			noMaterial = true;
		}
		else{
			heightIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.height;
			widthIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.width;
			noMaterial = false;
		}
	}
}
//
//
//

