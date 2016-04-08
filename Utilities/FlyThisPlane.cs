using UnityEngine;
using System.Collections;

public class FlyThisPlane : MonoBehaviour {
	public float turnSpeed = 30; // turning speed (degrees/second)
	public float moveSpeed = 20; // move speed
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



	}
	void FixedUpdate ()
	{
		GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * Quaternion.Euler (new Vector3 (Input.GetAxis ("Vertical") * turnSpeed * Time.deltaTime, 0, 0)));
		GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * Quaternion.Euler (new Vector3 (0, Input.GetAxis ("Horizontal") * turnSpeed * Time.deltaTime, 0)));
		GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * Quaternion.Euler (new Vector3 (0, 0, Input.GetAxis ("Tilt") * turnSpeed * Time.deltaTime)));
		GetComponent<Rigidbody>().velocity = (transform.forward.normalized * moveSpeed);

	}
}
