using UnityEngine;
using System.Collections;

public class FixYAxis : MonoBehaviour {
	
	public float y_axis_fix = 2;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool updatePosition = false;
		if (transform.position.y > y_axis_fix) {
			updatePosition = true;
		} else if (transform.position.y < y_axis_fix) {
			updatePosition = true;
		}
		
		if(updatePosition)
			transform.position = new Vector3(transform.position.x,
			                                y_axis_fix,
			                                transform.position.z);
	}
}
