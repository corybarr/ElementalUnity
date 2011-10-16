using UnityEngine;
using System.Collections;

public class FlyController : MonoBehaviour
{
	public float speed = 5.0f;
	
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * speed);
		transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * Time.deltaTime * speed);
	}
}
