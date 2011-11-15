using UnityEngine;
using System.Collections;

public class ClothMoverSetup : MonoBehaviour {
	public GameObject moverPrefab;
	private Transform[] clothMovers;
	private Transform interactiveCloth;

	public Vector3 prefabCubeSize = new Vector3(1.0f, 1.0f, 1.0f);
	private int numXCubes = 10;
	private int numZCubes = 10;
	private float leftmostEdge = -5.0f;

	// Use this for initialization
	void Start () {
		
		Vector3 objectSize = transform.localScale;;
		//Debug.Log("leftmostEdge is " + leftmostEdge);
		clothMovers = new Transform[numXCubes];
		
		for (int i=0; i < numXCubes; i++) {
			Vector3 moverPos = new Vector3(leftmostEdge + (float) i, 
			                               0.0f, 
			                               0.0f);
			GameObject newMover = (GameObject) Instantiate(moverPrefab,
			                                               moverPos,
		    	                                           Quaternion.identity);
			newMover.transform.parent = transform;
			clothMovers[i] = newMover.transform;
			
			interactiveCloth = gameObject.transform.Find("InteractiveCloth");
			interactiveCloth.GetComponent<AttachColliders>().setAttachedCollider(clothMovers[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
