using UnityEngine;
using System.Collections;

public class hideUponClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void hideMe () {
		this.transform.parent.gameObject.SetActive (false);
	}
}
