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
namespace AssemblyCSharp
{
	public abstract class Card : ICard
	{
		private int cardID;
		private string prefabName;

		public int getCardID ()
		{
			return this.cardID	;
		}

		public void setCardID(int cardID)
		{
			this.cardID = cardID;
		}

		public string getPrefabName ()
		{
			return this.prefabName;
		}

		public void setPrefabName (string preFabName)
		{
			this.prefabName = preFabName;
		}
	}
}
