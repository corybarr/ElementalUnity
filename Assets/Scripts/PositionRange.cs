using UnityEngine;
using System.Collections;

public class PositionRange : MonoBehaviour {
	
	public float min_x, min_y, min_z, max_x, max_y, max_z = 0f;
		
	// Use this for initialization
	void Start () {
		Debug.Log("This is working");
		//transform.position = new Vector3(5f, 1.02f, 0f);

	}

	void shiftCoords(Vector3 v3) {
		//Debug.Log("shifting coords by " + v3.x);
		bool changePos = false;
		float x_pos = transform.position.x + v3.x;
		float y_pos = transform.position.y + v3.y;
		float z_pos = transform.position.z + v3.z;
		
		if (x_pos < min_x) {
			x_pos = min_x;
		} else if (x_pos > max_x) {
			x_pos = max_x;
		}
		
		if (y_pos < min_y) {
			y_pos = min_y;
		} else if (y_pos > max_y) {
			y_pos = max_y;
		}
		
		if (z_pos < min_z) {
			z_pos = min_z;
		} else if (z_pos > max_z) {
			z_pos = max_z;
		}
	 
		transform.position = new Vector3(x_pos, y_pos, z_pos);
	}	
}
