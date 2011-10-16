using UnityEngine;
using System.Collections;

public class FloorCamera : MonoBehaviour {	
	void Update () {
		if (Input.GetButtonDown("Camera Toggle")) {
			if (camera.depth == 0) {
				camera.depth = -2;
			} else {
				camera.depth = 0;
			}
		} 
	}
}
