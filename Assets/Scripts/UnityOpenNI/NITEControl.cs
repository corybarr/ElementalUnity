using UnityEngine;
using System.Collections;
using NITE;

public class NITEControl : MonoBehaviour
{
	public NITESessionManager SessionManager;
	
	Broadcaster internalBroadcaster;
	
	protected void AddListener(MessageListener listener)
	{
		if (null == internalBroadcaster) internalBroadcaster = new Broadcaster();
		internalBroadcaster.AddListener(listener);
	}
	
	protected void RemoveListener(MessageListener listener)
	{
		if (null == internalBroadcaster) internalBroadcaster = new Broadcaster();
		internalBroadcaster.RemoveListener(listener);
	}
	
	void OnEnable()
	{
		if (null != SessionManager)
		{
			if (null == internalBroadcaster) internalBroadcaster = new Broadcaster();
			SessionManager.AddListener(internalBroadcaster);
		}
	}
		
	void OnDisable()
	{
		if (null != SessionManager)
		{
			if (null == internalBroadcaster) internalBroadcaster = new Broadcaster();
			SessionManager.RemoveListener(internalBroadcaster);
		}
	}

}
