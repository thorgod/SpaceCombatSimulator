using UnityEngine;
using System.Collections;

public class fakegravity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public float power=2;
	void OnTriggerStay(Collider other)
	{
		if (other.transform.parent != null && other.transform.parent.parent != null) {
			other.transform.parent.parent.GetComponent<Rigidbody>().AddForce(Vector3.down*power,ForceMode.Acceleration);
				}


	}
}
