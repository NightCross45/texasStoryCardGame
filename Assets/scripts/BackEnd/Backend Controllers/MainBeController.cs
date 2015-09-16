//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace AssemblyCSharp
{
	public class MainBeController: MonoBehaviour
	{
		private CharacterDeckController characterDeck;
		private PlayerController playerRoster;
		private int currentPlayerID;
		private frontEndManager frontEndManagerSCRIPT;


		#region Deck Formations
		public void FormCharacterDeck()
		{
			characterDeck = new CharacterDeckController ();
			characterDeck.CreateCharacterDeckFromXml ();
		}
		#endregion Deck Formations

		#region Player Formations
		public void FormPlayerRoster()
		{
			currentPlayerID = 0;
			playerRoster = new PlayerController ();
			for (int i =1; i <=4; i++) 
			{
				Player newPlayer = new Player();
				newPlayer.setPlayerID(i);
				playerRoster.addPlayerToGame(newPlayer);
			}
		}
		#endregion Player Formations


		#region Front End Formation
		public void FormFrontEnd()
		{
			frontEndManagerSCRIPT = GameObject.Find("frontEndManager").GetComponentInChildren<frontEndManager>();
		}
		#endregion Front End Formation

		#region Character Deck Events

		public CharacterCard DrawCharacterFromDeck(int cardID)
		{
			return characterDeck.drawCharacterCardFromDeck (cardID);
		}

		public void RemoveCharacterFromDeck(int cardID)
		{
			characterDeck.removeCharacterFromDeck (cardID);
		}

		public int GetTotalCardsInCharacterDeck()
		{
			return characterDeck.getTotalCards();
		}

		public void createCharacterRosterLineup()
		{
			//Debug.Log (CharacterDeck.getTotalCards());
			for (int i = 1; i <= GetTotalCardsInCharacterDeck(); i++) 
			{
				CharacterCard newCard = characterDeck.drawCharacterCardFromDeck(i);
				//FR_MANAGER.generateCharacterLineUpCard (newCard.getPrefabName (), newCard.getCardID (), GetTotalCardsInCharacterDeck());
				frontEndManagerSCRIPT.generateCharacterLineUpCard (newCard.getPrefabName (), newCard.getCardID (), GetTotalCardsInCharacterDeck());

			}
		}

		#endregion Character Deck Events


		#region Player Events

		public void PlayerChoseThisCharacterCard(int cardID)
		{
			currentPlayerID++;
			frontEndManagerSCRIPT.setPlayerTurn(currentPlayerID);
			playerRoster.setPlayerChosenCharacterCard (currentPlayerID,characterDeck.drawCharacterCardFromDeck (cardID));
			
			if (currentPlayerID == playerRoster.getTotalPlayers ()) 
			{
				frontEndManagerSCRIPT.endCharacterSelect();
			}
		}

		#endregion




	}
}

