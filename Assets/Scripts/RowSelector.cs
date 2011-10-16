using UnityEngine;
using System.Collections;

public class RowSelector : MonoBehaviour {

	public ColliderGroup colliderGroup;
	public ColliderCoordinates colliderCoordinates;
	
	void OnPointCloudCollisionEnter() 
	{
		Vector3 coordinates = colliderCoordinates.Coordinates;
		
		Transform[,,] colliders = colliderGroup.Colliders;
		
		int y = (int)coordinates.y;
		int z = (int)coordinates.z;
		for (int x = 0; x < colliders.GetLength(0); x++) {
			PointCloudFadingCollider fadingCollider = colliders[x,y,z].GetComponent(typeof(PointCloudFadingCollider)) as PointCloudFadingCollider;
			fadingCollider.Activation = 1.0f;
		}
	}
	
	void OnPointCloudCollisionExit() 
	{
		
	}
}
