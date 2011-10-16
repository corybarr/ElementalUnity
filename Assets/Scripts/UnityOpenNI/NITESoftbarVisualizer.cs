using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITESoftbarVisualizer : MonoBehaviour {

	public NITESoftbar Softbar;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if (Softbar.SliderAxis == Axis.X)
		{
			GUI.HorizontalSlider(new Rect(100, 300, 200, 40), Softbar.Value, 1.0f, 0);
		}
		else
		{
			GUI.VerticalSlider(new Rect(100, 300, 40, 200), Softbar.Value, 1.0f, 0);
		}
	}
}
