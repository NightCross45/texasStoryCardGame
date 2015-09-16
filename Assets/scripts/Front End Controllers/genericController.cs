using UnityEngine;
using System.Collections;

public class genericController : MonoBehaviour {

	public int cameraFunction = 0; // , 0 = do nothing, 1 = zoom, 2 = camera reset
	public Vector3 relativeCamPos = new Vector3 (0,0,0);

	void Start () {
		// Set custom relative offsets overriding anything set in Unity's gui.
		relativeCamPos.y = 1.2f;
		relativeCamPos.z = .1f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
      
}

