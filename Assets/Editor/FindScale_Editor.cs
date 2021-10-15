//
//		Parallax Scrolling VZ 
//---------------------------------------------------------------//
// 	 FindScale_Editor
// - uses PVZ_FindScale to find a textures dimensions, aspectratio
//   and the scale needed to set a pixel perfect x & y scale in
//   the inspector
//---------------------------------------------------------------//

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PVZ_FindScale))]
[CanEditMultipleObjects]
public class FindScale_Editor : Editor 
{
	float heightIs;				// used to find height of texture
	float widthIs;				// used to find the width of the texture
	// using either scaleX or scaleY will find you the other value for pixel-perfect scaling
	SerializedProperty scaleX;	// serialized property of the texture's X scale
	SerializedProperty scaleY;	// serialized property of the texture's Y scale
	float aspectRatio;			// aspect ratio of the textures dimensions
	public PVZ_FindScale myTarget;	// object of PVZ_FindScale to get variables
	bool noMaterial = false;		// determines if the object has a material attached - to prevent errors

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
		EditorGUILayout.LabelField("////    PVZ - FIND SCALE    ////");

		EditorGUILayout.LabelField("-----------------------------------------------");	
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("ATTACHED TEXTURE'S DIMENSIONS");
		
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
		// Set the target to reference variable from PVZ_FindScale
		myTarget = (PVZ_FindScale)target;

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

				noMaterial = true;
			}
			else{
				// If there is a texture attached then set local height and width to display
				heightIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.height;
				widthIs = myTarget.GetComponent<MeshRenderer>().sharedMaterial.mainTexture.width;
			}
		}
		// Get the scaleWidth & scaleHeight from PVZ_FindScale
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
