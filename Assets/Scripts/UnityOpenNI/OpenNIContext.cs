using UnityEngine;
using System;
using System.Collections;
using OpenNI;

public class OpenNIContext : MonoBehaviour
{
	static OpenNIContext instance;
	
	//static string oniFile = "C:\\Program Files\\OpenNI\\Samples\\Bin\\Release\\anticlockwise_walkaround.oni";
	static string oniFile = "";
	
	public static OpenNIContext Instance
	{
		get 
		{
			 if (null == instance)
            {
                instance = GameObject.FindObjectOfType(typeof(OpenNIContext)) as OpenNIContext;
                if (null == instance)
                {
                    GameObject container = new GameObject();
                    container.name = "OpenNIContextContainer";
                    instance = container.AddComponent(typeof(OpenNIContext)) as OpenNIContext;
                }
            }
			return instance;
		}
	}
	
	private Context context;
	public static Context Context 
	{
		get { return Instance.context; }
	}
	
	public DepthGenerator Depth;
	public bool Mirror
	{
		get { return mirror.IsMirrored(); }
		set { mirror.SetMirror(value); }
	}
	
	public static bool ValidContext
	{
		get { return instance != null; }
	}
	
	private MirrorCapability mirror;
	
	// private ctor for singleton
	private OpenNIContext()
	{
		this.context = new Context();
		if (null == context)
		{
			return;
		}
		
		if (oniFile != "") {
			context.OpenFileRecording(oniFile);
		}
		
		// NITE license from OpenNI.org
		License ll = new License();
		ll.Key = "0KOIk2JeIBYClPWVnMoRKn5cdY4=";
		ll.Vendor = "PrimeSense";
		context.AddLicense(ll);
				
		this.Depth = openNode(NodeType.Depth) as DepthGenerator;
		this.mirror = this.Depth.MirrorCapability;
		if (oniFile == "") {
			this.Mirror = true;
		}
	}

	private ProductionNode openNode(NodeType nt)
	{
		ProductionNode ret=null;
		try
		{	
			ret = context.FindExistingNode(nt);
		}
		catch
		{
			ret = context.CreateAnyProductionTree(nt, null);
			Generator g = ret as Generator;
			if (null != g)
			{
				g.StartGenerating();
			}
		}
		return ret;
	}
	
	public static ProductionNode OpenNode(NodeType nt)
	{
		return Instance.openNode(nt);
	}
	
	// (Since we add OpenNIContext singleton to a container GameObject, we get the MonoBehaviour functionality)
	public void Update () 
	{
		if (oniFile != "") {
			//this.context.WaitOneUpdateAll(this.Depth);
			this.context.WaitAnyUpdateAll();
		} else {
			this.context.WaitNoneUpdateAll();
		}
	}
	
	public void OnApplicationQuit()
	{
		context.StopGeneratingAll();
		OpenNIContext.instance = null;
	}
}
