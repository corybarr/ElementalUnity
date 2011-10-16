using UnityEngine;
using System.Collections;

public class AttractorMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//renderer.enable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnPointCloudCollisionEnter() {
		GameObject attractorObject = GameObject.FindWithTag("attractor");	
		attractorObject.transform.position = transform.position;				
	}
}
