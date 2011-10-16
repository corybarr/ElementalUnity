using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using OpenNI;
using Tao.OpenGl; 

public class OpenNIImagemapViewer : MonoBehaviour 
{
	private ImageGenerator Image;
	private Texture2D imageMapTexture;

	private ImageMetaData imageMetaData;
	private int imageLastFrameId;

	// Use this for initialization
	void Start () 
	{
		print("Graphics device: " + SystemInfo.graphicsDeviceVersion);
		Image = OpenNIContext.OpenNode(NodeType.Image) as ImageGenerator; //new ImageGenerator(OpenNIContext.Instance.context);
		imageMapTexture = new Texture2D( Image.MapOutputMode.XRes,  Image.MapOutputMode.YRes, TextureFormat.RGB24, false);		
		imageMetaData = new ImageMetaData();
	}
	
	// Update is called once per frame
	void Update () {
		// see if we have a new RGB frame
		Image.GetMetaData(imageMetaData);	
		if (imageMetaData.FrameID != imageLastFrameId)
		{
			imageLastFrameId = imageMetaData.FrameID;
			
			// TODO: Process RGB image here
			// Image.ImageMapPtr returns a pointer to the RGB data

			WriteImageToTexture(imageMapTexture,  Image.ImageMapPtr);
		}
		
	}
	
	void WriteImageToTexture (Texture2D tex, IntPtr p)
	{
		// NOTE: This method only works when unity is rendering with OpenGL ("unity.exe -force-opengl"). This is *much* faster
		// then Texture2D::SetPixels which we would have to use otherwise
		Gl.glBindTexture(Gl.GL_TEXTURE_2D, tex.GetNativeTextureID()+1);
		Gl.glTexSubImage2D(Gl.GL_TEXTURE_2D, 0, 0, 0, tex.width, tex.height, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, p);	
	}

   void OnGUI()
   {
		GUI.Box(new Rect(Screen.width - 128, Screen.height - 96,128,96), imageMapTexture);
   }
}
