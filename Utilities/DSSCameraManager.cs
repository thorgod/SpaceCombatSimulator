using UnityEngine;
using System.Collections;

public class DSSCameraManager : MonoBehaviour {

	private Camera[] cameras;
	public int cameraIndex;

	// Use this for initialization
	void Start () 
	{
		cameras = GetComponentsInChildren<Camera> ();
		cameraIndex = 5;


		cameraIndex = (cameraIndex + 1) % cameras.Length;
		for(int i=0;i<cameras.Length;i++)
		{
			if(i == cameraIndex)
				cameras[i].enabled = true;
			else
				cameras[i].enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		bool switchView = Input.GetKeyUp(KeyCode.V);
		if(switchView)
		{
			cameraIndex = (cameraIndex + 1) % cameras.Length;
			for(int i=0;i<cameras.Length;i++)
			{
				if(i == cameraIndex)
					cameras[i].enabled = true;
				else
					cameras[i].enabled = false;
			}
		}
	}
}
