using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenNI;

public class OpenNIDepthmapViewer : MonoBehaviour 
{
	public Color DepthMapColor = Color.yellow;
	public int XPos = 20;
	public int YPos = 20;
	public bool drawDepthMap = true;
	
	private Texture2D depthMapTexture;
	short[] rawDepthMap;

	float[] depthHistogramMap;
	Color[] depthMapPixels;
	int factor;
	int XRes;
	int YRes;

	// Use this for initialization
	void Start () 
	{
		// init texture
		MapOutputMode mom = OpenNIContext.Instance.Depth.MapOutputMode;
		factor = mom.XRes/160;
		YRes = mom.YRes;
		XRes = mom.XRes;
		
		depthMapTexture = new Texture2D((int)(mom.XRes/factor), (int)(mom.YRes/factor));
		
		// depthmap data
		rawDepthMap = new short[(int)(mom.XRes * mom.YRes)];
		depthMapPixels = new Color[(int)((mom.XRes/factor)*(mom.YRes/factor))];
		
		// histogram stuff
		int maxDepth = (int)OpenNIContext.Instance.Depth.DeviceMaxDepth;
		depthHistogramMap = new float[maxDepth];
	}
	
	// Update is called once per frame
	void Update () {
		//Context.Update();
		
		if (Input.GetButtonDown("Depth Toggle")) {
			drawDepthMap = !drawDepthMap;
		}  
	}
	
	void UpdateHistogram()
	{
		int i, numOfPoints = 0;
		
		Array.Clear(depthHistogramMap, 0, depthHistogramMap.Length);
		
		int YScaled = YRes/factor;
		int XScaled = XRes/factor;
		int depthIndex = 0;
		for (int y = 0; y < YScaled; ++y)
		{
			for (int x = 0; x < XScaled; ++x, depthIndex += factor)
			{
				if (rawDepthMap[depthIndex] != 0)
				{
					depthHistogramMap[rawDepthMap[depthIndex]]++;
					numOfPoints++;
				}
			}
			depthIndex += (factor-1)*XRes; // Skip lines
		}
/*
        for (i = 0; i < rawDepthMap.Length; i++)
        {
            // only calculate for valid depth
            if (rawDepthMap[i] != 0)
            {
                depthHistogramMap[rawDepthMap[i]]++;
                numOfPoints++;
            }
        }
		*/
        if (numOfPoints > 0)
        {
            for (i = 1; i < depthHistogramMap.Length; i++)
	        {   
		        depthHistogramMap[i] += depthHistogramMap[i-1];
	        }
            for (i = 0; i < depthHistogramMap.Length; i++)
	        {
                depthHistogramMap[i] = 1.0f - (depthHistogramMap[i] / numOfPoints);
	        }
        }
	}
	
	 void UpdateDepthmapTexture()
    {
		// flip the depthmap as we create the texture
		int YScaled = YRes/factor;
		int XScaled = XRes/factor;
		int i = XScaled*YScaled-1;
		int depthIndex = 0;
		for (int y = 0; y < YScaled; ++y)
		{
			for (int x = 0; x < XScaled; ++x, --i, depthIndex += factor)
			{
				short pixel = rawDepthMap[depthIndex];
				if (pixel == 0)
				{
					depthMapPixels[i] = Color.clear;
				}
				else
				{
					Color c = new Color(depthHistogramMap[pixel], depthHistogramMap[pixel], depthHistogramMap[pixel], 0.9f);
					depthMapPixels[i] = DepthMapColor * c;
				}
			}
			depthIndex += (factor-1)*XRes; // Skip lines
		}
        depthMapTexture.SetPixels(depthMapPixels);
        depthMapTexture.Apply();
   }
   
   void OnGUI()
   {
		if (!drawDepthMap) return;
		Marshal.Copy(OpenNIContext.Instance.Depth.DepthMapPtr, rawDepthMap, 0, rawDepthMap.Length);
		UpdateHistogram();
		UpdateDepthmapTexture();
		GUI.Box(new Rect(Screen.width - 128 - XPos, Screen.height - 96 -YPos,128,96), depthMapTexture);
		//GUI.DrawTexture(new Rect(Screen.width - 128 - 20, Screen.height - 96 -20,128,96), depthMapTexture);
   }
}
