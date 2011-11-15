using UnityEngine;
using System.Collections;

public class YAxisBobber : MonoBehaviour {
	
	float bobSpeed;
	float y_bob_min;
	float y_bob_max;
	float bob_direction = -1.0f;
	bool bob = false;
	
	// Use this for initialization
	void Start () {
		bobSpeed = (float) Random.Range(20, 100);
		bobSpeed /= 100.0f;
		
		y_bob_max = transform.localPosition.y;
		y_bob_min = y_bob_max - 3.0f;
		
		if (Random.Range(0, 100) > 75) {
			bob = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (bob) {
			float new_y_pos = transform.localPosition.y + bobSpeed * Time.deltaTime * bob_direction;
			if (new_y_pos < y_bob_min) {
				new_y_pos = y_bob_min;
				bob_direction = 1.0f;
			}
			else if (new_y_pos > y_bob_max) {
				new_y_pos = y_bob_max;
				bob_direction = -1.0f;
			}
		
			Vector3 pos = new Vector3(transform.localPosition.x, new_y_pos, transform.localScale.z);
			transform.localPosition = pos;
		}
	}
}
