using UnityEngine;
using System.Collections;

public class MoveOtherGameObject : MonoBehaviour {
	
	//public GameObject thingToMove;
	public GameObject thingToMove;
	public float x_amount = 0.003f;
	public float y_amount, z_amount = 0f;
	//private float x, y, z;
	
	
	// Use this for initialization
	/*
	void Start () {
		x = thingToMove.transform.position.x;
		y = thingToMove.transform.position.y;
		z = thingToMove.transform.position.z;
	}
	*/
	// Update is called once per frame
	/*void Update () {
		//thingToMove.transform.position = new Vector3(x, y, z);
	}
	*/

	void OnParticleCollision() {
		Vector3 v3 = new Vector3(x_amount, y_amount, z_amount);
		thingToMove.BroadcastMessage("shiftCoords", v3);
	}
}
