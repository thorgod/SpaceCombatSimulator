using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public GameObject target;
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
		Vector3 targetDir =  target.transform.position-transform.parent.transform.position;
		Vector3 forward = transform.parent.transform.forward;

		angle_to_object = Vector3.Angle (targetDir, forward).ToString();
	}
	string angle_to_object= "NA";
	// Update is called once per frame
	void OnGUI ()
	{
		GUI.TextField (new Rect (10, 10, 200, 20), angle_to_object,25);
	}
}
