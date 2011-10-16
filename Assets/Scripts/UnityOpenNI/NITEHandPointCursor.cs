using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITEHandPointCursor : NITEControl {

	private bool handPointActive;
	public bool HandPointActive
	{
		get { return handPointActive; }
	}
	
	public float Scale = 0.001f;
	
	private PointControl myControl;
	
	void Awake() 
	{
		myControl = new PointControl();
        myControl.PrimaryPointUpdate += new System.EventHandler<HandEventArgs>(myControl_PrimaryPointUpdate);
        myControl.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(myControl_PrimaryPointCreate);
        myControl.PrimaryPointDestroy += new System.EventHandler<IdEventArgs>(myControl_PrimaryPointDestroy);
		
		AddListener(myControl);
	}

    void myControl_PrimaryPointDestroy(object sender, IdEventArgs e)
    {
        handPointActive = false;
    }

    void myControl_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        handPointActive = true;
    }

    void myControl_PrimaryPointUpdate(object sender, HandEventArgs e)
    {
        Vector3 pos = new Vector3(e.Hand.Position.X, e.Hand.Position.Y, -e.Hand.Position.Z);
        Vector3 focusPoint = new Vector3(SessionManager.FocusPoint.X, SessionManager.FocusPoint.Y, -SessionManager.FocusPoint.Z);
        transform.position = (pos - focusPoint) * Scale;
    }

}
