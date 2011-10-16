using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class TerrainKinect : MonoBehaviour
{
	public Terrain terrain;
	public PointCloud pointCloud;
	
	private float[,] heights;

	void Start()
	{
		heights = new float[512, 512];
	}
	
	void Update()
	{
		int numPoints = pointCloud.NumPoints;
		Vector3[] points = pointCloud.Points;
		int xRes = pointCloud.XRes;
		
		int x = 0;
		int y = 0;
		for (int i = 0; i < numPoints; i++) 
		{ 
			if (x == xRes - 1) { 
				x = 0; 
				y++; 
			} else { 
				x++; 
			} 		
			if (x < 512 && y < 512) {
				if (points[i].z > 0) {
					heights[x, y] = 1 - points[i].z * 0.005f;
				} else {
					heights[x, y] = 0;
				}
			}
		} 	
		
		terrain.terrainData.SetHeights(0, 0, heights);
	}
}
