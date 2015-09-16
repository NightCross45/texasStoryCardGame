using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	public class frontEndManager : MonoBehaviour {

		public MainBeController MainBeControllerSCRIPT;
		public deckManager deckManagerSCRIPT ;
		public cameraController cameraControllerSCRIPT ;
		public diceController diceControllerSCRIPT ;

		public int playerTurn; 

		public GameObject startCharacterSelectBtn;

		void awake () {
		}

		// Init script references from other game objects, all other front end stuff accesses things through their definition here.
		void Start () {
			deckManagerSCRIPT = GameObject.Find("deckManager").GetComponentInChildren<deckManager>();
			MainBeControllerSCRIPT = GameObject.Find("MainBeController").GetComponentInChildren<MainBeController>();
			cameraControllerSCRIPT = GameObject.Find("mainCamera").GetComponentInChildren<cameraController>();
			diceControllerSCRIPT = GameObject.Find ("diceController").GetComponentInChildren<diceController>();

			startCharacterSelectBtn = GameObject.Find ("startCharacterSelectBtn");

		}

		void Update () {
			
		}

		// Backend calls this method, supplying an INT that indicates turn, and this method set's a frontEnd Variable to that number for reference.
		public void setPlayerTurn(int turn) {
			playerTurn = turn;
		}

		// End's character select, backEnd calls this when players chosen reaches 4, this should home camera, and start the normal rond layout visibilities.
		public void endCharacterSelect (){
			Debug.Log ("Ending Character select..");
		}

		// --------- generate the character line up cards. This method does one card at a time, the loop performs in the back end.
		public void generateCharacterLineUpCard (string prefabName, int cardID, int totalCards) {
			deckManagerSCRIPT.generateCharacterLineUpCard (prefabName, cardID, totalCards);
		}

		// --------- this method sends which card the player chose to the backend controller as an INT.
		public void PlayerChoseThisCharacterCard (int cardID) {
			MainBeControllerSCRIPT.PlayerChoseThisCharacterCard (cardID);
		}
	}
}
