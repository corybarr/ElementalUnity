using UnityEngine;
using System.Collections;

public class BlockRaiser : MonoBehaviour
{
	public PointCloudFadingCollider pointCloudFadingCollider;
	public float distance = 1.0f;

	private Vector3 startPos;

	void Start ()
	{
		startPos = transform.position;
	}

	void Update ()
	{
		transform.position = new Vector3 (startPos.x, 
		                                  startPos.y + (pointCloudFadingCollider.Activation * distance), 
		                                  startPos.z);
		if (pointCloudFadingCollider.Activation < 0.1f) {
			transform.position = startPos;
		}
	}
}
