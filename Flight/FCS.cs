using System;
using UnityEngine;
using System.Collections;

public class FCS : MonoBehaviour {
	
	public GUIText ScreenReadout;
	
	public Vector3 thrust = new Vector3(1,1,1);     //Total thrust per axis
	private Vector3 targetVelocity;                 //user input determines how fast user wants ship to rotate
	private Vector3 torques;                         //the amount of torque available for each axis, based on thrust
	public float maxRate = 8;                       //max desired turn rate
	private Vector3 curVelocity;                    //holds the rigidbody.angularVelocity converted from world space to local
	private Vector3 tActivation;			//switch vector to indicate which axes need thrust and in which direction (values are: -1, 0, or 1)
	
	public Vector3 Kp = new Vector3(8, 8, 8);
	public Vector3 Ki = new Vector3(.1f,.1f,.1f);
	public Vector3 Kd = new Vector3(.02f,.02f,.02f);

	public float rollInput  = 0;
	public float pitchInput = 0;
	public float yawInput   = 0;

	public float thrustInput = 0;
	public float vertInput   = 0;
	public float horizInput  = 0;


	
	private PidController3Axis pControl = new PidController3Axis();
	
	void Start() {
		//this is where the bounding box is used to create pseudo-realistic torque;  If you want more detail, just ask.
		var shipExtents = ((MeshFilter)GetComponentInChildren(typeof(MeshFilter))).mesh.bounds.extents;
		torques.x = new Vector2(shipExtents.y,shipExtents.z).magnitude*thrust.x;
		torques.y = new Vector2(shipExtents.x,shipExtents.z).magnitude*thrust.y;    //normally would be x and z, but mesh is rotated 90 degrees in mine.  
		torques.z = new Vector2(shipExtents.x,shipExtents.y).magnitude*thrust.z;    //normally would be x and y, but mesh is rotated 90 degrees in mine.
		
		ApplyValues();
	}
	
	void ApplyValues(){
		pControl.Kp = Kp;
		pControl.Ki = Ki;
		pControl.Kd = Kd;
		pControl.outputMax = torques;
		pControl.outputMin = torques * -1;
		pControl.SetBounds();
	}
	
	void RCS() {
		var h =  Input.GetAxis ("Mouse X");
		var v =  Input.GetAxis ("Mouse Y");
		GetComponent<Rigidbody>().AddRelativeTorque(h, 0, v);
		//transform.Rotate (h, 0, v);
		// Uncomment to catch inspector changes
		//ApplyValues();
		if (Input.GetKeyDown (KeyCode.A)) {
						GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
						GetComponent<Rigidbody>().velocity = Vector3.zero;
		} 


		// collect inputs

		yawInput    =Input.GetAxisRaw("Z_Rot");
		thrustInput = Input.GetAxisRaw("Thrust");
		vertInput   = Input.GetAxisRaw("RCS_V");
		horizInput  = Input.GetAxisRaw("RCS_H"); 		
		GetComponent<Rigidbody>().AddRelativeForce (horizInput*100, vertInput*100, thrustInput*200);

		
		//angular acceleration = torque/mass
		//var rates = torques/rigidbody.mass;
		
		//determine targer rates of rotation based on user input as a percentage of the maximum angular velocity
		targetVelocity = new Vector3(pitchInput*maxRate,yawInput*maxRate,rollInput*maxRate);
		
		//take the rigidbody.angularVelocity and convert it to local space; we need this for comparison to target rotation velocities
		curVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().angularVelocity);              
		
		// run the controller
		pControl.Cycle(curVelocity, targetVelocity, Time.fixedDeltaTime);
		GetComponent<Rigidbody>().AddRelativeTorque(pControl.output * Time.fixedDeltaTime, ForceMode.Impulse);
		
		if (ScreenReadout == null) return;
		ScreenReadout.text = "Current V : " + curVelocity + "\n"
			+ "Target V :" + pControl.output + "\n"
				+ "Current T : " + tActivation + "\n";

		
	}
	
	void FixedUpdate() {


		RCS();
	}
	
}

public class PidController3Axis {
	
	public Vector3 Kp;
	public Vector3 Ki;
	public Vector3 Kd;
	
	public Vector3 outputMax;
	public Vector3 outputMin;
	
	public Vector3 preError;
	
	public Vector3 integral;
	public Vector3 integralMax;
	public Vector3 integralMin;
	
	public Vector3 output;
	
	public void SetBounds(){
		integralMax = Divide(outputMax, Ki);
		integralMin = Divide(outputMin, Ki);       
	}
	
	public Vector3 Divide(Vector3 a, Vector3 b){
		Func<float, float> inv = (n) => 1/(n != 0? n : 1);
		var iVec = new Vector3(inv(b.x), inv(b.x), inv(b.z));
		return Vector3.Scale (a, iVec);
	}
	
	public Vector3 MinMax(Vector3 min, Vector3 max, Vector3 val){
		return Vector3.Min(Vector3.Max(min, val), max);
	}
	
	public Vector3 Cycle(Vector3 PV, Vector3 setpoint, float Dt){
		var error = setpoint - PV;
		integral = MinMax(integralMin, integralMax, integral + (error * Dt));
		
		var derivative = (error - preError) / Dt;
		output = Vector3.Scale(Kp,error) + Vector3.Scale(Ki,integral) + Vector3.Scale(Kd,derivative);
		output = MinMax(outputMin, outputMax, output);
		
		preError = error;
		return output;
	}
}
