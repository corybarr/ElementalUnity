using UnityEngine;
using System.Collections;

public class ColorCollisionIncreaser : MonoBehaviour {
	
	private Color myColor;
	private bool colorChanged = false;
	public int numHitsBeforeChange = 50;
	private int hitsCounter = 0;
	private bool redFull = false;
	private bool greenFull = false;
	private bool blueFull = false;
	
	// Use this for initialization
	void Start () {
		myColor.r = 0;
		myColor.g = 0;
		myColor.b = 0;
		renderer.material.color = myColor;	
	}			
		
	// Update is called once per frame
	void Update () {
		if (colorChanged) {
			renderer.material.color = myColor;
			colorChanged = false;
		}
	}
	
	void OnParticleCollision() {
		hitsCounter++;
		if (hitsCounter == numHitsBeforeChange) {
			hitsCounter = 0;

			if (!redFull) {
				myColor.r = myColor.r + 0.01f;
				colorChanged = true;
				if (myColor.r > 0.999f) redFull = true;
			} else if (!greenFull) {
				myColor.g = myColor.g + 0.01f;
				colorChanged = true;
				if (myColor.g >= 0.999f) greenFull = true;
			} else if (!blueFull) {
				myColor.b = myColor.b + 0.01f;
				colorChanged = true;
				if (myColor.b >= 0.999f) blueFull = true;
			}
		}
	}
}
