using UnityEngine;
using System.Collections;

public class GravityMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		 if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed left click.");
			Physics.gravity = new Vector3(-5.0f, 0f, 0f);
		}
	}
	
	
}
