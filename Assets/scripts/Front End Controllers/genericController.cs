using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{

	public class genericController : MonoBehaviour {

		public int cameraFunction = 0; // , 0 = do nothing, 1 = zoom, 2 = camera reset, 3 = test roll die
		public Vector3 relativeCamPos = new Vector3 (0,0,0);
		//frontEndManager frontEndManagerSCRIPT;

		void Start () {
			// Set custom relative offsets overriding anything set in Unity's gui.
			relativeCamPos.y = 1.2f;
			relativeCamPos.z = .1f;
			//frontEndManagerSCRIPT = GameObject.Find("frontEndManager").GetComponentInChildren<frontEndManager>();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void reRollDie () {

		}
	      
	}

}

