using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{

	public class diceController : MonoBehaviour {
		
		List<GameObject> diceList= new List<GameObject>();

		//private GameObject activeDice1;
		//private GameObject activeDice2;

		private Vector3 startingPos = new Vector3(0,5,0);
		private Vector3 endingPos = new Vector3(0,0,0);
		private Vector3 velocity = Vector3.zero;
		private float distanceToTarget;
		private float smoothTime = .1f;

		public float diceLandSpacing = 1f;
		public float diceThrowDelay = .3f;

		void Start () {

			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll1", typeof(GameObject)));
			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll2", typeof(GameObject)));
			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll3", typeof(GameObject)));
			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll4", typeof(GameObject)));
			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll5", typeof(GameObject)));
			diceList.Add((GameObject) Resources.Load("prefabs/Dice/diceRoll6", typeof(GameObject)));

			int rollVal1 = Random.Range (0,6);
			int rollVal2 = Random.Range (0,6);

			StartCoroutine (startStaggeredDiceRoll (rollVal1, rollVal2, diceThrowDelay, diceLandSpacing));

		}

		public void rollDie () {

			foreach (Transform child in transform)
			{
				//child is your child transform
				if(child.gameObject != null) {
					Destroy(child.gameObject);
				}
			}
			int rollVal1 = Random.Range (0,6);
			int rollVal2 = Random.Range (0,6);
			StartCoroutine (startStaggeredDiceRoll (rollVal1, rollVal2, diceThrowDelay, diceLandSpacing));

		}
		
		// Update is called once per frame
		void Update () {
		
		}

		IEnumerator startStaggeredDiceRoll(int v1, int v2, float delay, float spacing ) {
			StartCoroutine( singleDiceRoll (v1, smoothTime, endingPos, spacing*-1));
			yield return new WaitForSeconds(delay);
			StartCoroutine( singleDiceRoll (v2, smoothTime, endingPos, spacing*1 ));
			yield return null;
		}


		IEnumerator singleDiceRoll(int diceRoll, float smoothTime, Vector3 end, float spacing) {
			GameObject item = Instantiate(diceList[diceRoll]) as GameObject;
			item.transform.parent = this.gameObject.transform;
			Vector3 modifiedEndingPos = end + new Vector3(spacing,0,0);
			item.transform.localPosition = startingPos;
			Debug.Log (item.transform.localPosition);
			float distanceToTarget = Vector3.Distance (item.transform.localPosition, modifiedEndingPos);
			Vector3 velocity = Vector3.zero;
			while (distanceToTarget >= .01) {
				item.transform.localPosition = Vector3.SmoothDamp (item.transform.localPosition, modifiedEndingPos, ref velocity, smoothTime);
				distanceToTarget = Vector3.Distance (item.transform.localPosition, modifiedEndingPos);
				yield return null;
			}

			
		}


	}

}