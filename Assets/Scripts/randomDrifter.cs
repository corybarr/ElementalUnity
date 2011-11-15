using UnityEngine;
using System.Collections;

public class randomDrifter : MonoBehaviour {
	
	public bool randomDrift = false;
	float driftSpeed = 0.5f;
	
	float x_dir = 1.0f;
	float z_dir = 1.0f;
	
	// Use this for initialization
	void Start () {		
		if (Random.Range(0, 2) == 1 ) {
			x_dir *= -1.0f;
		}
		if (Random.Range(0, 2) == 1) {
			z_dir *= -1.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (randomDrift) {
			Vector3 newPos = new Vector3(transform.localPosition.x + x_dir * driftSpeed * Time.deltaTime,
			                             transform.localPosition.y,
			                             transform.localPosition.z + z_dir * driftSpeed * Time.deltaTime);
			transform.localPosition = newPos;
		}
	}
}
