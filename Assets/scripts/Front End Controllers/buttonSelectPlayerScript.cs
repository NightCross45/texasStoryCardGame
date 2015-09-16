using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{

	public class buttonSelectPlayerScript : MonoBehaviour {

		frontEndManager frontEndManagerSCRIPT;

		void awake (){

		}

		// We only need to init the frontEndManager, any other scripts we need to access we access through frontEndGameManager
		void Start () {
			frontEndManagerSCRIPT = GameObject.Find("frontEndManager").GetComponentInChildren<frontEndManager>();
		}

		// Update is called once per frame - dont really need this atm.
		void Update () {
		
		}

		public void buttonSelectPlayerFunction(){

			// GET REFERENCE TO ACTUAL PARENT CARD PREFAB (this. is the button script reference.)
			GameObject parentCard = this.transform.parent.parent.gameObject;

			// ------------- PASS CHARACTER ID TO BACK END
			int selectedCharacter = parentCard.GetComponentInChildren<cardController>().cardID;
			frontEndManagerSCRIPT.PlayerChoseThisCharacterCard (selectedCharacter);

			// ------------- FIND AND PLACE CHARACTER CARD UNDER CORRECT PLAYER SPOT
			string playerGameObject = "player" + frontEndManagerSCRIPT.playerTurn; // make gameObject string
			Transform targetPlayerGameObject = GameObject.Find(playerGameObject).transform.GetChild(0); // use string to get target transform
			parentCard.transform.SetParent(targetPlayerGameObject); // set parent card's parent to target. 
			parentCard.transform.localPosition = new Vector3(0,0,0); // zero out local transforms since overall placement is handled from parents.
			parentCard.transform.localEulerAngles = new Vector3(0,0,0); // set rotations to 0 as well, since they are also handled by parents.
			frontEndManagerSCRIPT.cameraControllerSCRIPT.goToCharacterSelect(); // reset cam back to home. 
			parentCard.GetComponentInChildren<cardController>().releaseFocus(); // this releases this card's focus turning off the gui attached to it.

		
		}

	}

}
