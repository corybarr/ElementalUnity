using UnityEngine;
using System.Collections;

public class OSCTestSender : MonoBehaviour {

	private Osc oscHandler;
	public OscBalls oscBallsGO;
	private bool moveObject;
	
	public string remoteIp;
	public int senderPort;
	public int listenerPort; 
	
	public delegate void MidiEventReceiver(string status, int byte1, int byte2);
	
	public MidiEventReceiver midiEventReceiver;
	
	
	~OSCTestSender() {
		print("Destructor called");
		if (oscHandler != null) {
			oscHandler.Cancel();
		}
		
		oscHandler = null;
		System.GC.Collect();
	} 
	
	// Use this for initialization
	void Start () {
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(remoteIp, senderPort, listenerPort);
		
		oscHandler = GetComponent<Osc>();
		oscHandler.init(udp);
		oscHandler.SetAddressHandler("/ballcolor", Example);
		
		//oscBallsGO = GameObject.FindWithTag("oscBallGroup");
		moveObject = true;
	}
	
	// Update is called once per frame
	void Update () {
		//DEVEL: GET RID OF THIS
		if (Input.GetButtonDown("Depth Toggle")) {
			print("OSC Sender button pressed");
			moveSphere();
		} 
		
		if (moveObject) {
			moveSphere();
			moveObject = false;
		}
	}
	
	void onDisable() {
		oscHandler.Cancel();
		oscHandler = null;
	}
	
	public void SendNoteOn(int noteNum) {
		
		OscMessage oscM = Osc.StringToOscMessage("/noteon " +  noteNum + " 120");
		oscHandler.Send(oscM);
	}
	
	public void SendNoteOff(int noteNum) {
		OscMessage oscM = Osc.StringToOscMessage("/noteoff " + noteNum);
		oscHandler.Send(oscM);
	}
	
	//DEVEL: get rid of this
	private void moveSphere() {
		oscBallsGO.transform.Translate(1.0f, 1.0f, 1.0f);
	}
	
	public void Example(OscMessage m) {
		//print("----------> OSC example message received: (" + m + ")");
		string osc_report_string = "";
		//print("OSC message: " + Osc.OscMessageToString(m) + "\n");
		//int val3 = (int) m.Values[3];
		//int val3 = (int) 3;
		//print("Values[0]: " + m.Values[0] + ", Values[3] (converted): " + val3 + "\n");
		//print("Values.Count: " + m.Values.Count + "\n");
		//print("osc_report_string: " + osc_report_string + "\n");
		for (int i = 0; i < m.Values.Count; i++) {
			osc_report_string = osc_report_string + "Values[" + i + "]: " + m.Values[i] + "***";
		}
		print("osc_report_string: " + osc_report_string + "\n");
		
		//moveSphere();
		//moveObject = true;
		
		midiEventReceiver((string)m.Values[0], (int)m.Values[1], (int)m.Values[2]);
		//changeBallColor(4, 65);
	}
	
	private void changeBallColor(int ball_num, int color) {
		oscBallsGO.changeBallColor(4,65);
		
		
	}
}
