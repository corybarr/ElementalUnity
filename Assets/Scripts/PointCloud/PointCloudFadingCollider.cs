using UnityEngine;
using System.Collections;

public class PointCloudFadingCollider : MonoBehaviour {
	// NOTE: Don't even try to run this until you've trimmed the point cloud down considerably (<2000 points)

	static float activationSpeed = 10f;
	static int skip = 500;	
	
	private bool isCollided = false;	
	public float Activation = 0.0f;
	
	public PointCloud pointCloud;
	
	bool isMouseActivated = false;
	
	void Start () {
		Activation = 0.0f;
		if (pointCloud == null) {
			Debug.Log("Point cloud is not assigned to collider.");
		}
	}
	
	void Update() {
	
		if (pointCloud == null) {
			// Handle mouse activation for when the Kinect isn't hooked up
			if (isMouseActivated) {
				IncreaseActivation();
			} else {
				DecreaseActivation();
			}
			return;
		}
		
		Vector3[] points = pointCloud.Points;
		int numPoints = pointCloud.NumPoints;
		
		bool collisionFound = false;
		
		for (int i = 0; i < numPoints; i += skip) {	
			if (collider.bounds.Contains(points[i])) {
				collisionFound = true;
				break;
			}
		}
		if (!isCollided && collisionFound) {
			IncreaseActivation();
		} else if (!collisionFound) {
			DecreaseActivation();
		}
	}
	
	void IncreaseActivation() 
	{
		Activation += activationSpeed * Time.deltaTime;
		if (Activation > 1.0f) {
			Activation = 1.0f;
			gameObject.SendMessage("OnPointCloudCollisionEnter");
			isCollided = true;				
		}
	}
	
	void DecreaseActivation()
	{
		if (Activation > 0) {
			Activation -= activationSpeed / 30.0f * Time.deltaTime;
		}
		if (isCollided && Activation < 0) {
			Activation = 0;
			gameObject.SendMessage("OnPointCloudCollisionExit");
			isCollided = false;				
		}
	}

	void OnMouseEnter() 
	{
		isMouseActivated = true;
	}
	
	void OnMouseExit() 
	{
		isMouseActivated = false;
	}
}
