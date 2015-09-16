using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
	
	public class deckManager : MonoBehaviour 
	{
		// formatting vars
		public float horizontalCardSpacing = .85f;
		public float verticalCardSpacing = 1.25f;
		public float additionalHorizontalOffset = 0f;
		public float additionalVerticalOffset = -1.0f;
		public int cardsPerRow = 5;
		public float transitionTime = .2f;
		public float horizOffsetCounter = 0f;
		public float vertOffsetCounter = 0f;
		public int numRows = 0;
		public int x = 0;
		public int y = 0;
		
		// animation vars
		Vector3 spawnCoords;
		Vector3 startPos = new Vector3(0,0,0);
		Vector3 endPos = new Vector3(0,0,0);
		
		// turn vars, only used for physically placing cards on table. Although should eventually get this from back end at every event
		//public int playerChooseTurn = 1;

		// -------------- generate lineup cards, this is executed once per lineup card, loop happens in backEnd. ----------------
		public void generateCharacterLineUpCard (string prefabName, int cardID, int totalCards) {
			
			// The next three lines we calculated number of rows, and the vertical and horizontal offset based on card ID and other values declared above.
			numRows = (int)((float)totalCards / (float)cardsPerRow);
			vertOffsetCounter = (((float)y * verticalCardSpacing) + ((float)(numRows-1) * verticalCardSpacing * -.5f) + additionalVerticalOffset)*-1;
			horizOffsetCounter = ((float)x * horizontalCardSpacing) + ((float)(cardsPerRow-1) * horizontalCardSpacing * -.5f) + additionalHorizontalOffset;
			
			Vector3 offsetAmt = new Vector3(horizOffsetCounter, .1f, vertOffsetCounter); // calculated relative, offset amount to be applied to this card.
			endPos = new Vector3 (horizOffsetCounter, .1f, vertOffsetCounter); // calculated end position this card will end up at, passed to coroutine for animation.
			
			GameObject card = (GameObject) Resources.Load("prefabs/"+prefabName, typeof(GameObject)); // Load the prefab card from Resources
			GameObject item = Instantiate(card) as GameObject; // actually instantiate the loaded card as a game object.
			item.transform.parent = this.gameObject.transform; // parent card to deck manager - (after being instantiated it is free floating with no parent)
			item.transform.localPosition = startPos; // Set our newly instantiated card's position to the card spawn location's placeholder.
			
			// Instantiate player card GUI
			GameObject cardGUI = (GameObject) Resources.Load("prefabs/CharacterCardCanvas", typeof(GameObject)); // now we load the card GUI prefab manually
			GameObject cardGUIObj = Instantiate(cardGUI) as GameObject; // and then we instantiate the card GUI as another object
			
			cardGUIObj.transform.SetParent (item.transform, false); // Since it is instantiated free floating, we set the GUI's parent to this current card.
			cardGUIObj.transform.localPosition = new Vector3 (0f, 0f, 0.13f); // Set the gui slightly above 0 for zfighting issues.
			cardGUIObj.SetActive (false); // set to active so we can see and interact with it.
			cardController cardControllerScript = item.GetComponentInChildren<cardController>();// get the card controller script in the recently made card
			StartCoroutine( startCardFlourish(item.transform, transitionTime, endPos) );// start flourish animation of card to destination.

			// Iterate the x and y pos of the card by using the card id and cardsPerRow
			x = cardID % cardsPerRow;
			y = cardID / cardsPerRow;
		}

		// Card flourish courotine definition. Animates card to destination until it's distance is within threshold.
		IEnumerator startCardFlourish(Transform card, float smoothTime, Vector3 end) {
			float distanceToTarget = Vector3.Distance (card.localPosition, end);
			Vector3 velocity = Vector3.zero;
			while (distanceToTarget >= .01) {
				card.localPosition = Vector3.SmoothDamp (card.localPosition, end, ref velocity, smoothTime);
				distanceToTarget = Vector3.Distance (card.localPosition, end);
				yield return null;
			}
			
		}
		
		
		// Use this for initialization
		void Start () {
			// we get the spawn coords as a vector3 and save it to a conveniant variable.
			startPos = this.gameObject.transform.FindChild("CardSpawnLocation").transform.localPosition;
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
	}
	
}

