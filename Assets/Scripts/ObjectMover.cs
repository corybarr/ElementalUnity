using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {
	
	public float max_x, max_y, max_z, min_x, min_y, min_z = 0f;
	public float vecMagnitude = 1.25f;
	private Vector3 objectTrajectory;
	
	// Use this for initialization
	void Start () {
		objectTrajectory = new Vector3(vecMagnitude, 0f, vecMagnitude);
		rigidbody.velocity = objectTrajectory;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
