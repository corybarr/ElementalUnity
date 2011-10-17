using UnityEngine;
using System.Collections;

public class OscBalls : MonoBehaviour
{
	public GameObject prefabOSCSphere;
	public int numBalls = 5;

	public Transform[] Balls {
		get { return balls; }
	}
	private Transform[] balls;


	// Use this for initialization
	void Start ()
	{
		
		prefabOSCSphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

			balls = new Transform[numBalls];
		for (int i = 0; i < numBalls; i++) {
			
			float offset = 0.3f;
			
			GameObject newBall = (GameObject)Instantiate (prefabOSCSphere, 
			                                              new Vector3 (offset * (float)i, 
			                                                           4.0f, 
			                                                           offset * (float)i), 
			                                              Quaternion.identity);
			newBall.transform.parent = transform;
//			ballCoordinates coordinates = newBall.GetComponent (typeof(ballCoordinates)) as ColliderCoordinates;
//			coordinates.Coordinates = new Vector3 (i, i, i);
			balls[i] = newBall.transform;
		}
		
		
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void changeBallColor (int ball_num, int color) {
		Color myColor = new Color();
		myColor.b = color;
		
		ball_num %= numBalls;
		balls[ball_num].gameObject.renderer.material.color = myColor;

	}
}
