using UnityEngine;
using System.Collections;

public class OSCTestSender : MonoBehaviour {

	private Osc oscHandler;
	private GameObject respawn;
	
	public string remoteIp;
	public int senderPort;
	public int listenerPort; 
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
		
		respawn = GameObject.FindWithTag("testSphere");
	}
	
	// Update is called once per frame
	void Update () {
		//DEVEL: GET RID OF THIS
		if (Input.GetButtonDown("Depth Toggle")) {
			print("OSC Sender button pressed");
			moveSphere();
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
		respawn.transform.Translate(1.0f, 1.0f, 1.0f);
	}
	
	public void Example(OscMessage m) {
		print("----------> OSC example message received: (" + m + ")");
		moveSphere();
	}
}
