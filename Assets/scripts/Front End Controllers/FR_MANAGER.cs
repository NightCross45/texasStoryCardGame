using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{

	public static class FR_MANAGER {
		//public static CharacterSelectManager crManager = new CharacterSelectManager();
		public static deckManager deckManagerSCRIPT = GameObject.Find("deckManager").GetComponentInChildren<deckManager>();

		public static void generateCharacterLineUpCard(string prefabName, int cardID, int totalCards) 
		{
			//crManager.generateCharacterLineUpCard (prefabName, cardID, totalCards);
			//characterSelectManagerSCRIPT.generateCharacterLineUpCard (prefabName, cardID, totalCards);
		}
	}
}