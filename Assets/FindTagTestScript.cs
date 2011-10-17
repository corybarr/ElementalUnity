using UnityEngine;
using System.Collections;

public class FindTagTestScript : MonoBehaviour {
	
	
    private GameObject respawn;// = GameObject.FindWithTag("testSphere");
    void Awake() {
        //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;
    }

	
	
	// Use this for initialization
	void Start () {
	respawn = GameObject.FindWithTag("testSphere");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		print("The mouse was clicked.\n");
		respawn.transform.Translate(1.0f, 1.0f, 1.0f);
    }
}
