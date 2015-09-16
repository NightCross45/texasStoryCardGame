using UnityEngine;
using System.Collections;

public class cardController : MonoBehaviour {

	Renderer cardPrefab;
	public int cardID = -1; // initially set cardID to -1, this should always be set to something other than -1 upon initializing.

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// IF we mouse over a the gameObject this is attached to.
	void OnMouseEnter(){
		cardPrefab = GetComponentInChildren<Renderer>();
		cardPrefab.material.SetColor("_Color", Color.red);
	}

	// IF we mouse back out of the gameObject this is attached to.
	void OnMouseExit(){
		cardPrefab = GetComponentInChildren<Renderer>();
		cardPrefab.material.SetColor("_Color", Color.white);
	}

	// focus is set here, the only child (hence (0) has it's active flag set to true)
	public void setFocus (){
		this.transform.GetChild (0).gameObject.SetActive(true); // activate gui
	}

	// focus is set here, the only child (hence (0) has it's active flag set to false)
	public void releaseFocus (){
		//Debug.Log ("Released Focus...");
		this.transform.GetChild (0).gameObject.SetActive(false); // deactivate gui
	}
}

