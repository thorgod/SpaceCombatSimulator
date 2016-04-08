using UnityEngine;
using System.Collections;

public class MoveCameraWithMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	float horizontalSpeed  = 40.0f;
	float verticalSpeed = 40.0f;
	// Update is called once per frame
	void Update () {
		// Move Camera Script
		

		

			
			// If Right Button is clicked Camera will move.
			while (Input.GetKeyDown(KeyCode.X)) {
				 float v= horizontalSpeed * Input.GetAxis ("Mouse Y");
				 float h= verticalSpeed * Input.GetAxis ("Mouse X");
				transform.Translate(v,h,0);
			}
			
	}
}
