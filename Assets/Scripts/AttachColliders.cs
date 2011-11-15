using UnityEngine;
using System.Collections;

public class AttachColliders : MonoBehaviour {
	private InteractiveCloth ic;
	
	
	// Use this for initialization
	void Start () {
		ic = GetComponent<InteractiveCloth>() as InteractiveCloth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
//	void setAttachedCollider(Transform t, int num) {
	public void setAttachedCollider(Transform t) {
		//Debug.Log("setAttachedCollider() called");
		ic.AttachToCollider(t.collider);
	}
}
