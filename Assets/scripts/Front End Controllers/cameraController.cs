using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{

	public class cameraController : MonoBehaviour {

		private Vector3 camDefaultPosition = new Vector3(0,10,0); // home position for camera. 
		private Vector3 camCharacterSelectPosition = new Vector3(0,7,0); // position for character select.
		// TODO: add camera rotations, or get from player seats.
		// Paramters for camera animation.
		private Vector3 camStartPosition = new Vector3(0,0,0);
		private Vector3 camDestination = new Vector3(0,0,0);
		private Vector3 velocity = Vector3.zero;
		private float distanceToTarget;
		private float smoothTime = .1f;
		private int cameraIsBusy = 0;

		// Game mechanics stuffs
		int gameMode = 0; // 0 = player select, 1 = regular game, 2 = r&D TODO: this may change, prolly will to receive info from backend
		GameObject lastObjectToHaveFocus;

		frontEndManager frontEndManagerSCRIPT ;

		void awake() {
		}

		void Start () {
			resetCamPosition ();
			frontEndManagerSCRIPT = GameObject.Find("frontEndManager").GetComponentInChildren<frontEndManager>();
		}

		// A courotine for animation the camera from stasrt to finish. "cameraIsBusy" keeps another anim from starting when set to 1.
		IEnumerator startCameraMove() {
			distanceToTarget = Vector3.Distance(Camera.main.transform.position, camDestination);
			cameraIsBusy = 1;
			while(distanceToTarget >= .01) {
				Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, camDestination, ref velocity, smoothTime);
				distanceToTarget = Vector3.Distance(Camera.main.transform.position, camDestination);
				yield return null;
			}
			cameraIsBusy = 0;
		}

		// method for resetting cam position to home.
		public void resetCamPosition(){
			//Debug.Log ("asdasd");
			camStartPosition = Camera.main.transform.position;
			camDestination = camDefaultPosition;
			StartCoroutine("startCameraMove");
		}

		// method for resetting cam position to characterSelect.
		public void goToCharacterSelect(){
			//Debug.Log ("asdasd");
			camStartPosition = Camera.main.transform.position;
			camDestination = camCharacterSelectPosition;
			StartCoroutine("startCameraMove");
		}

		// Update happens every frame, this is neccesary to constantly check for user interaction updates.
		void Update () {

			// if camera is NOT busy... proceed.
			if(cameraIsBusy == 0) {

				// If the left mouse button is  pressed.... proceed.
				if (Input.GetMouseButtonDown (0)) {

					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // shoot a ray into the scene at the click.
					RaycastHit hit; // create ray object.

					// If the ray hit a gameobjects collider... proceed. (NOTE : a click can't be detected unless an Game object has a physics collider attached to it.)
					if (Physics.Raycast(ray, out hit)) {

						// If our game mode is == 0, AKA if we are in PLAYER SELECT, ....proceed.
						if (gameMode == 0) {

							// hit.collider.gameobject is how one fetches the clicked upon game object. From there all other navigation is the same as GO.search(), etc.
							cardController cardController = hit.collider.gameObject.GetComponentInChildren<cardController>(); // get card controller script in selected object.
							genericController genericControllerScript = hit.collider.gameObject.GetComponentInChildren<genericController>(); // also get the generic controller.

							// IF card controller and generic controller are present and not null.... proceed.
							if(genericControllerScript && cardController){

								// if generic controller script's function is set to "ZOOM" .... proceed.
								if(genericControllerScript.cameraFunction == 1){

									// If we were previously zoomed on another card..... proceed.
									if(lastObjectToHaveFocus){
										lastObjectToHaveFocus.GetComponentInChildren<cardController>().releaseFocus(); // release focus, hide it's UI, and unzoom from current card.
									}

									// set new cam Start and End positions via relative selected object and current pos.
									camDestination = genericControllerScript.relativeCamPos + hit.collider.gameObject.transform.position;
									camStartPosition = Camera.main.transform.position;
									StartCoroutine("startCameraMove"); // start the camera move animation.

									cardController.setFocus(); // set focus to newly zoomed and selected card, so we now see it's GUI and stats.
									lastObjectToHaveFocus = hit.collider.gameObject; // set our last object to have focus var for next click.
								}

								// IF instead our generic controller's function was set to 0, it means we clicked on nothing ... proceed.
								if(genericControllerScript.cameraFunction == 0){

									// If we were previously zoomed on an object and it had focus.... proceed.
									if(lastObjectToHaveFocus){

										// get the cardController of the last item in focus and release it's focus.
										lastObjectToHaveFocus.GetComponentInChildren<cardController>().releaseFocus();
										lastObjectToHaveFocus = null; // now set lastObjectToHaveFocus to 0;
									}
								}

								// If we are set to 3, we test roll die again.... 
								if(genericControllerScript.cameraFunction == 3){
									frontEndManagerSCRIPT.diceControllerSCRIPT.rollDie();
								}

							}
						}

						// This game Mode is currently set to be an R&D mode that skips all the above logic. good for testing simple executions or new methods.
						if (gameMode == 2) {
							GameObject hitObj = hit.collider.gameObject;
							hitObj.transform.Translate(Vector3.forward * Time.deltaTime);
						}
					}

					// IF the ray did NOT hit something, then we either clicked scenery that does not react or we clicked on nothing.
					else {
						resetCamPosition(); // reset cam to HOME

						// IF there was an object currently in focus..... proceed.
						if(lastObjectToHaveFocus){
							lastObjectToHaveFocus.GetComponentInChildren<cardController>().releaseFocus(); // get the card controller of last focused and release it.
							lastObjectToHaveFocus = null; // then set lastObjectTOHaveFocus to null since nothing is in focus now.
						}
					}
				
				}
			}
		}
	} 

}