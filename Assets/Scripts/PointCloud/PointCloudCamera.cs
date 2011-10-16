using UnityEngine;
using System.Collections;
using Tao.OpenGl;

public class PointCloudCamera : MonoBehaviour {
	public PointCloud pointCloud;
	public bool drawCloud = true;	
	public bool drawCalibrationCube = true;
	public float cubeScale = 10.0f;
	public bool cycleColors = true;
	
	private Material mat;	
	
	void OnPostRender() 
	{
		DrawToCamera(camera);
	}
	
	void OnDrawGizmos()
	{
		DrawToCamera(Camera.current);
	}
	
	void DrawToCamera(Camera cam)
	{
		if (!mat) {
			mat = new Material(Shader.Find("VertexLit"));
		}
		mat.SetPass (0);
		
		SetupGl();
		LoadCameraMatrix(cam);
		if (drawCalibrationCube) {
			DrawCalibrationCube();
		}
		if (drawCloud) {
			DrawPointCloud();	
		}
	}
	
	void SetupGl() 
	{
		Gl.glDisable(Gl.GL_TEXTURE_2D);
		Gl.glDisable(Gl.GL_LIGHTING);
		Gl.glDisable(Gl.GL_COLOR_MATERIAL);
		Gl.glLoadIdentity();
	}
	
	void LoadCameraMatrix(Camera cam)
	{
		Matrix4x4 m = cam.worldToCameraMatrix;
		float[] matrix = new float[16];
		for (int i = 0; i < 16; i++) {
			matrix[i] = m[i];
		}
		Gl.glLoadMatrixf (matrix);
	}
	
	void DrawPointCloud() 
	{
		Vector3[] points = pointCloud.Points;
		int numPoints = pointCloud.NumPoints;
		
		// Draw the points
		Gl.glBegin(Gl.GL_POINTS);
		Gl.glColor3f (0, 0, 1.0f);
		int skip = 0;
		for (int i = 0; i < numPoints; i++) {
			if (points[i] == Vector3.zero) {
				continue;
			}
			float px = points[i].x;
			float py = points[i].y;
			float pz = points[i].z;
					
			if (cycleColors && skip > 3) {
				Gl.glColor3f((Mathf.Sin(pz*1.5f) + 1) * 0.5f, 
					 	  (Mathf.Sin(pz*1.0f) + 1) * 0.5f, 
					  	  (Mathf.Sin(pz*0.5f) + 1) * 0.5f);		
				skip = 0;
			} else if (cycleColors) {	
				skip++;
			}
			Gl.glVertex3f (px, py, pz);
		}
		Gl.glEnd();
	}
	
	void DrawCalibrationCube()
	{
		Gl.glPushMatrix();
		Gl.glScalef(cubeScale, cubeScale, cubeScale);
		
		Gl.glColor3f (1.0f, 0.0f, 0.0f);
		Gl.glBegin (Gl.GL_LINE_LOOP);
		Gl.glVertex3f (-0.5f, -0.5f, -0.5f);		
		Gl.glVertex3f (-0.5f, 0.5f, -0.5f);
		Gl.glVertex3f (0.5f, 0.5f, -0.5f);
		Gl.glVertex3f (0.5f, -0.5f, -0.5f);
		Gl.glEnd ();
		
		Gl.glBegin (Gl.GL_LINE_LOOP);
		Gl.glVertex3f (-0.5f, -0.5f, 0.5f);
		Gl.glVertex3f (-0.5f, 0.5f, 0.5f);
		Gl.glVertex3f (0.5f, 0.5f, 0.5f);
		Gl.glVertex3f (0.5f, -0.5f, 0.5f);
		Gl.glEnd ();
		
		Gl.glBegin (Gl.GL_LINES);
		Gl.glVertex3f (-0.5f, -0.5f, -0.5f);
		Gl.glVertex3f (-0.5f, -0.5f, 0.5f);
		Gl.glVertex3f (-0.5f, 0.5f, -0.5f);
		Gl.glVertex3f (-0.5f, 0.5f, 0.5f);
		Gl.glVertex3f (0.5f, 0.5f, -0.5f);
		Gl.glVertex3f (0.5f, 0.5f, 0.5f);
		Gl.glVertex3f (0.5f, -0.5f, -0.5f);
		Gl.glVertex3f (0.5f, -0.5f, 0.5f);
		Gl.glEnd ();
		
		Gl.glPopMatrix();
	}
}
