using UnityEngine;
using System.Collections;

public class simpleRotator : MonoBehaviour {
	
	//public float xRotation1 = 0.0f;
	public float RotationSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

	void Update()
		{
		transform.Rotate (Vector3.up * (RotationSpeed * Time.deltaTime));
	}
}
