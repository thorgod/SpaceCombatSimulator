using UnityEngine;
using System.Collections;

public class GUI_HUDS : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().centerOfMass = new Vector3 (-39.54823f, 0.9112439f, 28.28926f);

	}
	public float AmbientSpeed = 100.0f;
	float timeLeftOnThrust = 0.0f;
	public float RotationSpeed = 200.0f;

	public float RotationSpeedV = .07f;
	public float RotationSpeedH = .05f;

	public bool selectScreenMode = true;
	void  Update()
	{

		if (Input.GetMouseButtonDown (2)) {
					selectScreenMode = !selectScreenMode;
		}
		if (Input.GetKey (KeyCode.Escape)) 
		{
			selectScreenMode = !selectScreenMode;
		} 
	}
	// Update is called once per frame
	void FixedUpdate() {



		if(selectScreenMode)
		{
			Screen.lockCursor = false;
			return;
			//rigidbody.velocity = (rigidbody.velocity*.95f)+ Vector3.Project (rigidbody.velocity*RotationSpeedH, transform.forward);
			//
		}

		Screen.lockCursor = true;

			

		/*Quaternion AddRot = Quaternion.identity;
		float roll = 0;
		float pitch = 0;
		float yaw = 0;
		roll = Input.GetAxis("Yaw") * (Time.deltaTime * RotationSpeed);
		pitch = Input.GetAxis("Vertical") * (Time.deltaTime * RotationSpeed);
		yaw = Input.GetAxis("Horizontal") * (Time.deltaTime * RotationSpeed);
		AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
		rigidbody.rotation *= AddRot;
		Vector3 AddPos = Vector3.forward;
		AddPos = rigidbody.rotation * AddPos;
		rigidbody.velocity = new Vector3(AddPos.x*rigidbody.velocity.x,AddPos.y*rigidbody.velocity.x,AddPos.z*rigidbody.velocity.x);
		rigidbody.AddRelativeForce(0,0,Input.GetAxis ("Thrust")*2000);
*/
		float vertical = Input.GetAxis ("Vertical");
		float horizontal =Input.GetAxis ("Horizontal");
		float yaw = Input.GetAxis ("Yaw");
		float thrust = Input.GetAxis ("Thrust");
		float mouse1 = Input.GetAxis ("Break");
		bool mouse = Input.GetMouseButton (0);

		float up =Input.GetAxis ("Up");


			//rigidbody.angularVelocity = Vector3.zero;
			//rigidbody.velocity = Vector3.zero;
		if (isGliding || mouse) 
		{
			GetComponent<Rigidbody>().AddRelativeForce (0, 0, (thrust) * 100f);
			GetComponent<Rigidbody>().AddRelativeTorque (vertical *50* RotationSpeedV, horizontal *50* RotationSpeedH, yaw * 25);
			if (mouse && isGliding ) 
			{
				GetComponent<Rigidbody>().velocity = (GetComponent<Rigidbody>().velocity*.95f)+ Vector3.Project (GetComponent<Rigidbody>().velocity*.05f, transform.forward);

			}
		}
		else  
		{
			if(GetComponent<Rigidbody>().velocity.magnitude < 200 || noSpeedLimit)
			{
				GetComponent<Rigidbody>().AddRelativeForce(0,0,(thrust)*100f);
			}

			
			float mag = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude,150,200);
			
			mag = (220 - mag)/2;



			if(mouse1 > 0)
			{
				GetComponent<Rigidbody>().AddRelativeTorque(vertical*mag*(RotationSpeedV+.05f) ,horizontal*mag*(RotationSpeedH+.05f),  yaw*15);
			}
			else
			{
				GetComponent<Rigidbody>().AddRelativeTorque(vertical*mag*RotationSpeedV ,horizontal*mag*RotationSpeedH,  yaw*15, ForceMode.Force);
			}
			
			GetComponent<Rigidbody>().velocity = (GetComponent<Rigidbody>().velocity*.80f)+ Vector3.Project (GetComponent<Rigidbody>().velocity*.20f, transform.forward);
			//rigidbody.velocity =Vector3.Project (rigidbody.velocity, transform.forward);

			//Quaternion direction = transform.rotation;
			//Quaternion get = Quaternion.

		}


		if (mouse1>0  ) {
			doStop ();
		} else {
			Flares(transform, "airbreak", false);
			Flares (transform, "StopTop", false);
		}

		if (vertical > 0) {
			 

			Flares(transform, "TopTop", true);
		}
		else
		{
			Flares(transform, "TopTop",false);
		}

		if (vertical < 0) {
			Flares(transform, "TopBot", true);
		}
		else
		{
			Flares(transform, "TopBot",false);
		}

		if (horizontal < 0) {
			Flares(transform, "TopLeft", true);
		}
		else
		{
			Flares(transform, "TopLeft",false);
		}

		if (horizontal > 0) {
			Flares(transform, "TopRight", true);
		}
		else
		{
			Flares(transform, "TopRight",false);
		}
		if (yaw>0) 
		{
			Flares(transform, "RotateClock", true);
		} 
		else 
		{
			Flares(transform, "RotateClock", false);
		}
		if (yaw<0) 
		{
			Flares(transform, "RotateCC", true);
		} 
		else 
		{
			Flares(transform, "RotateCC", false);
		}
		foreach (Transform t in transform)
		{
			if(t.name == "Afterburner Left")// Do something to child one
				
			{
				foreach (Transform a in t)
				{
					if(a.name == "Glow")
					{
						timeLeftOnThrust -= Time.deltaTime;
						ParticleSystem system = t.GetComponent<ParticleSystem>();
						ParticleSystem system2 = a.GetComponent<ParticleSystem>();
						
						if(thrust>0)
						{
							system.emissionRate = 80*thrust;//system.emissionRate = Mathf.Lerp(80,0,5f);
							system2.emissionRate = 80*thrust;//system2.emissionRate = Mathf.Lerp(80,0,5f);
						}
						else if(timeLeftOnThrust < 0)
						{
							system.emissionRate = 0;
							system2.emissionRate = 0;
						}
					}
				}
				
			}
		}




	}
	private float strength = 0f;
	private float smoothTime = 35.0f;
	void doStop()
	{
		Mathf.SmoothDamp(0.0f, 1.0f,ref strength, smoothTime);
		Vector3 f = -(GetComponent<Rigidbody>().mass * GetComponent<Rigidbody>().velocity) * strength;
		GetComponent<Rigidbody>().AddForce(f, ForceMode.Impulse);
		Flares(transform, "airbreak", true);
		Flares (transform, "StopTop", true);
	}

	void Flares(Transform Transforms, string name, bool on)
	 {
		foreach (Transform t in Transforms)
		{
			if(t.name == name)// Do something to child one
				
			{
				foreach (Transform a in t)
				{
					if(a.name == "Glow")
					{
						
						ParticleSystem system = t.GetComponent<ParticleSystem>();
						ParticleSystem system2 = a.GetComponent<ParticleSystem>();
						
						if(on)
						{
							system.emissionRate = 80;
							system2.emissionRate = 80;
						}
						else 
						{
							system.emissionRate = 0;
							system2.emissionRate = 0;
						}
					}
				}
				
			}
		}
	}
	bool noSpeedLimit = false;
	bool isGliding = false;

	string TextIt = "Glide";
	// Update is called once per frame
	void OnGUI ()
	{

		GUI.TextField (new Rect (10, 10, 200, 20), GetComponent<Rigidbody>().velocity.magnitude.ToString("0") + " Ft/Sec",25);

		if (selectScreenMode) {
			if (GUI.Button (new Rect (10, 40, 70, 20), "Start")) 
			{
				selectScreenMode = false;
			}
		}


		if (GUI.Button (new Rect (10, 60, 70, 20), "Cheat")) 
		{
			noSpeedLimit = !noSpeedLimit;
		}


		if (GUI.Button (new Rect (10, 80, 70, 20), TextIt)) 
		{
			isGliding = !isGliding;
			if(!isGliding)
			{
				Screen.lockCursor = true;
				TextIt="Glide";
			}
			else
			{
				Screen.lockCursor = false;
				TextIt="Auto";
			}

		}
		if (GUI.Button(new Rect(10, 100, 70, 20), "Restart"))
		{
			
			Application.LoadLevel (Application.loadedLevelName);
			
		}
		if (GUI.Button (new Rect (10, 120, 70, 20), "Dont Press")) {
			Application.Quit();
		}

		if (GUI.Button (new Rect (10, 140, 70, 20), "Exit")) {
			Application.Quit();
		}
		
		
	}


}
