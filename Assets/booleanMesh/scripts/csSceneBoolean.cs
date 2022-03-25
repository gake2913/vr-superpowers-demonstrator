﻿using UnityEngine;
using System.Collections;

public class csSceneBoolean : MonoBehaviour {

	public MeshCollider meshColliderA;
	public MeshCollider meshColliderB;

	// Use this for initialization
	void Start () {

		// Create new GameObject
		GameObject newObject = new GameObject();
		newObject.transform.localScale*=2f;
		MeshFilter meshFilter = newObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = newObject.AddComponent<MeshRenderer>();
		meshRenderer.materials = new Material[2]{meshColliderA.transform.GetComponent<Renderer>().materials[0],meshColliderB.transform.GetComponent<Renderer>().materials[0]};
	
		// Assign booleanMesh
		BooleanMesh booleanMesh = new BooleanMesh(meshColliderA,meshColliderB);
		//meshFilter.mesh = booleanMesh.Difference();
		//meshFilter.mesh = booleanMesh.Union();
		meshFilter.mesh = booleanMesh.Intersection();
	
	}	

}
