using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenNI;

public class PointCloud : MonoBehaviour
{
	public Vector3 bias = new Vector3(0, 0, -200);
	public Vector3 boundMax = new Vector3(100, 100, 100);
	public Vector3 boundMin = new Vector3(-100, -100, -100);
	
	private int maxPoints;
	
	private int numPoints;
	public int NumPoints {
		get { return numPoints; }
	}
	private Vector3[] points;
	public Vector3[] Points {
		get { return points; }
	}
	private int xRes;
	public int XRes {
		get { return xRes; }
	}
	private int yRes;
	public int YRes {
		get { return yRes; }
	}
	private short[] rawDepthMap;

	void Start ()
	{
		print ("Graphics device: " + SystemInfo.graphicsDeviceVersion);
		
		MapOutputMode mom = OpenNIContext.Instance.Depth.MapOutputMode;
		yRes = mom.YRes;
		xRes = mom.XRes;
		
		maxPoints = xRes * yRes;
		points = new Vector3[maxPoints];
		
		rawDepthMap = new short[(int)(mom.XRes * mom.YRes)];
	}

	void Update ()
	{
		ProcessPoints ();
	}

	void ProcessPoints ()
	{
		DepthGenerator depth = OpenNIContext.Instance.Depth;
		
		Marshal.Copy (depth.DepthMapPtr, rawDepthMap, 0, rawDepthMap.Length);
		
		int focalLength = (int)depth.GetIntProperty ("ZPD");
		double pixelSize = depth.GetRealProperty ("ZPPS");
		
		//Find all pixels 
		numPoints = 0;
		int p = 0;
		int x = 0;
		int y = 0;
		Matrix4x4 m = transform.localToWorldMatrix;

		float adj = (float)pixelSize * 0.1f / focalLength;
		for (int i = 0; i < maxPoints; i++) {
			if (x == xRes - 1) {
				x = 0;
				y++;
			} else {
				x++;
			}
			points[p].x = (x - 320) * rawDepthMap[i] * adj;
			points[p].y = (y - 240) * rawDepthMap[i] * adj;
			points[p].z = rawDepthMap[i] * 0.1f;				
			
			points[p] += bias;
			points[p] = m.MultiplyPoint3x4 (points[p]);
			
			if ((points[p].x < boundMin.x || points[p].x > boundMax.x) ||
				(points[p].y < boundMin.y || points[p].y > boundMax.y) ||
			    (points[p].z < boundMin.z || points[p].z > boundMax.z)) {
				// point is outside bounds
			} else {
				p++;
			}
		}
		numPoints = p;
	}
}
