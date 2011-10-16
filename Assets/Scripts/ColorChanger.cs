using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
	public Color onColor = Color.yellow;
	public Color offColor = Color.black;
	public float colorRange = 0.2f;
	
	public PointCloudFadingCollider pointCloudFadingCollider;
	
	void Start () {
		renderer.material.color = offColor;
	}
	
	void Update() {
		if (pointCloudFadingCollider.Activation >= 0.75f) {
			Color newColor; 
			newColor = onColor;
			newColor.a = 1.0f;
			renderer.material.color = newColor;
		} else {
			Color newColor = Color.Lerp(offColor, onColor, pointCloudFadingCollider.Activation);
			newColor.a = pointCloudFadingCollider.Activation + 0.1f;
			renderer.material.color = newColor;
		}
	}
	
	void OnPointCloudCollisionEnter() {
		onColor.r *= Random.Range(1.0f - colorRange, 1.0f + colorRange);
		onColor.g *= Random.Range(1.0f - colorRange, 1.0f + colorRange);
		onColor.b *= Random.Range(1.0f - colorRange, 1.0f + colorRange);

		//renderer.material.color = onColor;
	}
	
	void OnPointCloudCollisionExit() {
	}
}
