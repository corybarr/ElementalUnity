using UnityEngine;
using System.Collections;

public class OscBalls : MonoBehaviour
{
	public GameObject prefabOSCSphere;
	public int numBalls = 5;

	public GameObject[] Balls {
		get { return balls; }
	}
	private GameObject[] balls;
	
	public OSCTestSender testSender;

	private Color[] ballColors;
	
	public void myMidiEventMethod(string status, int byte1, int byte2) {
		print("MidiEvent  " + status + "  byte1:  " + byte1 + "byte2:  " + byte2);
		changeBallColor(byte1, byte2);
	}


	// Use this for initialization
	void Start ()
	{
		
		ballColors = new Color[5];
		
		print("testSender:   " + testSender);
		
		testSender.midiEventReceiver = new OSCTestSender.MidiEventReceiver(myMidiEventMethod);
		
		prefabOSCSphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

			balls = new GameObject[numBalls];
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
			balls[i] = newBall;
			
			ballColors[i] = new Color(0, 0, 127);
			//newBall.renderer.material.color = new Color(0, 0, 127);
		}
		
		
	}

	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < numBalls; i++) {
			balls[i].renderer.material.color = ballColors[i];	
		}
	}
	
	public void changeBallColor (int ball_num, int color) {
		Color myColor = new Color();
		myColor.b = color;
		
		ball_num %= numBalls;
		
		ballColors[ball_num] = myColor;
		//balls[ball_num].renderer.material.color = myColor;
		
		

	}
}
