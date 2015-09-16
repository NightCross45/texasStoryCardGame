using UnityEngine;
using System.Collections;

public class lightFlickerController : MonoBehaviour {


	private float heightScale = 1.0F;
	private float xScale = 1.0F;

	private float fHeightScale = 2.0F;
	private float fScale = 2.0F;
	private float flickerSpeed = 6.0f;

	public Light lt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float x = heightScale * Mathf.PerlinNoise(((Time.time * xScale)-(xScale*.5f)), 0.0f);
		float y = heightScale * Mathf.PerlinNoise(((Time.time * xScale)-(xScale*.5f)), 2.0f);
		Vector3 pos = transform.position;
		pos.x = x;
		pos.y = 5.3f;
		pos.z = y;
		transform.position = pos;

		float i = (fHeightScale * Mathf.PerlinNoise(Time.time*flickerSpeed, 4.0f)) + 10.0f;
		lt = GetComponent<Light>();
		lt.range = i;
	}
}
