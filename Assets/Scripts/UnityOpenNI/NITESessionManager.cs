using UnityEngine;
using System.Collections;
using OpenNI;
using NITE;

public class NITESessionManager : MonoBehaviour 
{
	SessionManager sessionManager;
	Broadcaster broadcaster;
	
	private bool isInSession;
	public bool IsInSession
	{
		get { return isInSession; }
	}
	
	public Point3D FocusPoint
	{
		get { return sessionManager.FocusPoint; }
	}
	
	public void AddListener(MessageListener listener)
	{
		if (null == broadcaster) broadcaster = new Broadcaster();
		broadcaster.AddListener(listener);
	}
	
	public void RemoveListener(MessageListener listener)
	{
		if (null == broadcaster) broadcaster = new Broadcaster();
		broadcaster.RemoveListener(listener);
	}
	
	public int QuickRefocusTimeout
	{
		get { return sessionManager.QuickRefocusTimeout ; }
		set { sessionManager.QuickRefocusTimeout = value; }
	}
		
	void Awake()
	{
		if (null == broadcaster) broadcaster = new Broadcaster();
	}
	
	// Use this for initialization
	void Start () {
	
		// create hand & gesture generators, if we dont have them yet
		OpenNIContext.OpenNode(NodeType.Hands);
		OpenNIContext.OpenNode(NodeType.Gesture);

		// init session manager
		sessionManager = new SessionManager(OpenNIContext.Context, "Click", "RaiseHand");
		sessionManager.SessionStart += new System.EventHandler<PositionEventArgs>(sessionManager_SessionStart);
		sessionManager.SessionEnd += new System.EventHandler(sessionManager_SessionEnd);
		
		sessionManager.AddListener(broadcaster);
	}

	void sessionManager_SessionEnd(object sender, System.EventArgs e)
	{
		isInSession = false;
		// TODO: event
	}

	void sessionManager_SessionStart(object sender, PositionEventArgs e)
	{
		isInSession = true;
		// TODO: event
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (OpenNIContext.ValidContext)
		{
			sessionManager.Update(OpenNIContext.Context);
		}
	}
}
