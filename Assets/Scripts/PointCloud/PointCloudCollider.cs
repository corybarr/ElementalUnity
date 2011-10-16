using UnityEngine;
using System.Collections;

public class PointCloudCollider : MonoBehaviour {
	// NOTE: Don't even try to run this until you've trimmed the point cloud down considerably (<2000 points)

	public PointCloud pointCloud;
	static int skip = 500;	
	
	private bool isCollided = false;
	
	void Start () {
	}
	
	void Update () {
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
			gameObject.SendMessage("PointCloudCollisionEnter");
			isCollided = true;
		} else if (isCollided && !collisionFound) {
			gameObject.SendMessage("PointCloudCollisionExit");
			isCollided = false;
		}
	}
}
