using UnityEngine;
using System.Collections;

public class OscBalls : MonoBehaviour
{
	
	//these should all be public after devel
	public GameObject prefabOSCSphere;
	private int numBalls = 127;
	private int ball_ncols = 12;
	private float ball_offset = 0.3f;
	
	private bool[] needs_update;

	public Transform[] Balls {
		get { return balls; }
	}
	private Transform[] balls;
	
	public OSCTestSender testSender;

	private Color[] ballColors;
	
	public void myMidiEventMethod(string status, int byte1, int byte2) {
		if(status == "note_off") {
			print("MidiEvent  " + status + "  byte1:  " + byte1 + "byte2:  " + byte2);
		}
		
		int ball_num = byte1;
		int ball_color = byte2;
		if (status == "note_off") {
			ball_color = 0;
		}
		
		changeBallColor(ball_num, ball_color);
	}


	// Use this for initialization
	void Start ()
	{
		needs_update = new bool[numBalls];
		for (int i=0; i < numBalls; i++) {
			needs_update[i] = false;
		}
		
		ballColors = new Color[numBalls];
		
		print("testSender:   " + testSender);
		
		testSender.midiEventReceiver = new OSCTestSender.MidiEventReceiver(myMidiEventMethod);
		
		//prefabOSCSphere.transform.localScale = new Vector3(0.3f, 0, 0.3f);
		
		
		balls = new Transform[numBalls];
		int ball_col_num = 0;
		Vector3 parent_transform_pos = transform.position;
		for (int i = 0; i < numBalls; i++) {

			int ball_rownum = i / ball_ncols;
			int ball_colnum = i % ball_ncols;
			
			//transform.
			
			Vector3 ball_pos = new Vector3 (ball_offset * (float)ball_colnum, 
			                  				4.0f, 
			                                ball_offset * (float)ball_rownum);
			ball_pos = ball_pos + parent_transform_pos;
			GameObject newBall = (GameObject)Instantiate (prefabOSCSphere, 
			                                              ball_pos, 
			                                              Quaternion.identity);
			newBall.transform.parent = transform;
			balls[i] = newBall.transform;
			
			ballColors[i] = new Color(0, 0, 0);
		}
		
		
	}

	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < numBalls; i++) {
			if (needs_update[i]) {
				balls[i].renderer.material.color = ballColors[i];
					
				float max_x, max_y, max_z, min_x, min_y, min_z = 0f;
				float vecMagnitude = 1.25f;
				Vector3 objectTrajectory;
	
				// Use this for initialization
	
				objectTrajectory = new Vector3(Random.Range(-0.75f, 0.75f) * vecMagnitude, 
				                               Random.Range(0.1f, 3.7f) * vecMagnitude, 
				                               Random.Range(-0.75f, 0.75f) * vecMagnitude);
				
				
				 //Compenent.RigidBody ourRigidbody = gameObject.GetComponent <Rigidbody>();
				balls[i].rigidbody.velocity = objectTrajectory;
				print ("rigidbody: " + balls[i].rigidbody +  "   this:  " + this  + "  objectTrajectory:  " + objectTrajectory);
		
				needs_update[i] = false;
			}
		}
	}
	
	public void changeBallColor (int ball_num, int color) {
		Color myColor = new Color();
		myColor.b = color;
		
		ball_num %= numBalls;

	
		ballColors[ball_num] = myColor;
		//balls[ball_num].renderer.material.color = myColor;
		needs_update[ball_num] = true;
		

		
		

	}
}
