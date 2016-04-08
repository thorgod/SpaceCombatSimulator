using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.Text;

public class TimerNumberTwo : MonoBehaviour {
	float time;
	float lapTime;
	bool started = false;
	int currentwaypoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
						time = Time.time - lapTime;
				}
	}
	void OnTriggerEnter( Collider hit){

		if (hit.gameObject.tag == "StartCheckPoint") {
						StartNewLap ();

		} 
		else if (hit.gameObject.tag == "CheckPoint" && started) 
		{
			if (hit.gameObject.name == currentwaypoint.ToString ()) {
								currentwaypoint++;
			} else 
			{
								int forpass = 0;
								int.TryParse (hit.gameObject.name, out forpass);
								Debug.Log ((forpass - currentwaypoint) * 10);
								lapTime -= (forpass - currentwaypoint) * 10;
								currentwaypoint = forpass;
			}
		} 
		else if (hit.gameObject.tag == "EndCheckPoint"&& started) {
			if(currentwaypoint>=17)
			{
				currentwaypoint++;
				started = false;

				string str = time.ToString("0.00000");
				Debug.Log(str);
				byte[] encodedbytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
				string encoded = System.Convert.ToBase64String(encodedbytes);
				Debug.Log(encoded);
				Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
					//Application.OpenURL("http://www.mikes3ds.com/score.php?item="+encoded);

				byte[] tobedecodedbytes = System.Convert.FromBase64String(encoded);
				string decoded = System.Text.ASCIIEncoding.ASCII.GetString(tobedecodedbytes);
				Debug.Log(decoded);    

			}
		}

	}
	


	
	void OnGUI(){
		GUI.Box (new Rect (100,100,210,20), "Current lap time:" + time.ToString("0.000"));
		GUI.Box (new Rect (100,120,210,20), "Check Point " + currentwaypoint +"/18");
	}
	
	void StartNewLap(){
		currentwaypoint = 1;
		lapTime = Time.time;
		started = true;
	}
}
