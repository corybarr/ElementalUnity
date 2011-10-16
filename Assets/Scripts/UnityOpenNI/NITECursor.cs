using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITECursor : NITEControl 
{
	SelectableSlider2D cursorSlider;
	SelectableSlider1D clickSlider;

        public float ClickSliderSize = 100.0f;
	// this is my "cheap" alternative to a full blown event queue
	// (probably not the best solution, but works for our purposes)
	public bool HandledLastClick;
	
	bool handPointActive;
	public bool CursorActive
	{
		get { return handPointActive; }
	}
	
	private Vector2 cursorPosition;
	public Vector2 CursorPosition
	{	
		get { return cursorPosition; }
	}
	
	private bool isClicked;
	public bool IsClicked
	{
		get { return isClicked; }
	}
	
	private bool isClickPush;
	public bool IsClickPush
	{
		get { return isClickPush; }
	}
	
	private bool isClickRelease;
	public bool IsClickRelease
	{
		get { return isClickRelease; }
	}
	
	private float clickProgress;
	public float ClickProgress
	{
		get { return clickProgress; }
	}
	
	private Vector2 clickPosition;
	public Vector2 ClickPosition
	{
		get { return clickPosition; }
	}

	// Use this for initialization
	void Awake() {
		cursorSlider = new SelectableSlider2D(1,1);
		cursorSlider.ValueChangeOnOffAxis = true;
        cursorSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(cursorSlider_PrimaryPointCreate);
        cursorSlider.PrimaryPointDestroy += new System.EventHandler<IdEventArgs>(cursorSlider_PrimaryPointDestroy);
        cursorSlider.ValueChange += new System.EventHandler<Value2EventArgs>(cursorSlider_ValueChange);
		
		// we create the click slider as a separate slider so we can move it around independently from the cursorSlider
		clickSlider = new SelectableSlider1D(1, 0, Axis.Z, false, 0, ClickSliderSize, 0, "Something");
		clickSlider.ValueChangeOnOffAxis = true;
        clickSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(clickSlider_PrimaryPointCreate);
        clickSlider.PrimaryPointUpdate += new System.EventHandler<HandEventArgs>(clickSlider_PrimaryPointUpdate);
        clickSlider.ValueChange += new System.EventHandler<ValueEventArgs>(clickSlider_ValueChange);
		
		AddListener(cursorSlider);
		AddListener(clickSlider);
		
		cursorPosition = new Vector2(0,0);
		clickPosition = new Vector2(0,0);
		NITEGUI.NiteCursor = this;
	}

    void clickSlider_ValueChange(object sender, ValueEventArgs e)
    {
        isClickPush = false;
        isClickRelease = false;

        if (!isClicked)
        {
            if (e.Value == 0.0f)
            {
                isClickPush = true;
                isClicked = true;
                clickPosition = cursorPosition;
            }
            else
            {
                clickProgress = 1.0f - e.Value;
            }
        }
        else
        {
            if (e.Value > 0.1)
            {
                isClicked = false;
                isClickRelease = true;
                HandledLastClick = false;
            }
        }

        clickProgress = 1.0f - e.Value;
    }

    void clickSlider_PrimaryPointUpdate(object sender, HandEventArgs e)
    {
        // we want the slider to "drift" and follow the hand position, so that it always feels responsive
        Point3D clickSliderCenter = clickSlider.Center;
        float halfSliderSize = clickSlider.SliderSize * 0.5f;

        // hand is in front of slider
        if (e.Hand.Position.Z < (clickSliderCenter.Z - halfSliderSize))
        {
            // move slider towards sensor
            clickSliderCenter.Z = e.Hand.Position.Z + halfSliderSize;
        }

        // hand is behind the slider
        else if (e.Hand.Position.Z > (clickSliderCenter.Z + halfSliderSize))
        {
            // move slider away from sensor
            clickSliderCenter.Z = e.Hand.Position.Z - halfSliderSize;
        }

        // hand is within slider range, but we aren't clicked yet
        else if (!IsClicked)
        {
            // "drift" the slider to the comfortable 0.7 position, that way selection is never too far away
            // (move the center of the slider to 0.2 closer to the sensor than current hand position)
            float deltaZ = (e.Hand.Position.Z - (clickSlider.SliderSize * 0.2f)) - clickSliderCenter.Z;
            clickSliderCenter.Z += (deltaZ * 0.02f);
        }

        clickSlider.Center = clickSliderCenter;
    }

    void clickSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        clickSlider.Center = focusPoint;
    }

    void cursorSlider_ValueChange(object sender, Value2EventArgs e)
    {
        cursorPosition.x = e.ValueX;
        cursorPosition.y = 1.0f - e.ValueY;
    }

    void cursorSlider_PrimaryPointDestroy(object sender, IdEventArgs e)
    {
        handPointActive = false;
    }

    void cursorSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        cursorSlider.Center = focusPoint;
        handPointActive = true;
    }

}
