using UnityEngine;
using System.Collections;

public class BallRepeller : MonoBehaviour {
	public float power = 5.0f;
	public float radius = 10.0f;
	public PointCloudFadingCollider pointCloudFadingCollider;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision collision) 
	{
		rigidbody.AddExplosionForce (power, transform.position, radius, 3.0f);
	}
}
