using UnityEngine;
using System.Collections;

public class CustomFollow : MonoBehaviour {

	public Vector3 m_CameraTarget;
	public float m_Distance = 10.0f;
	public float xSpeed = 4.0f;
	public float ySpeed = 4.0f;
	public int zoomMultiplier = 3;
	public int yMinLimit = -50;
	public int yMaxLimit = 80;
	public float minDist = 5.0f;
	public float maxDist = 20.0f;
	public bool canInteract = true;
	public Transform followTarget;
	private SmoothFollowCSharp smoothFollow;
	private float m_x = 0.0f;
	private float m_y = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
		if (followTarget) {
			smoothFollow = followTarget.GetComponent<SmoothFollowCSharp> ();
		}
		
		// inicial rotation of the camera == camera current editor rotation
		m_x = transform.rotation.eulerAngles.y;
		m_y = transform.rotation.eulerAngles.x;
		
		UpdateCamera ();
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (Screen.lockCursor)
			return;
		
		if (Input.GetKey (KeyCode.LeftControl)) {
			// do nothing!
		} else {
			
			if (Input.GetMouseButton (0) && canInteract) {
				
				m_x += Input.GetAxis ("Mouse X") * xSpeed;
				m_y -= Input.GetAxis ("Mouse Y") * ySpeed;
				m_y = ClampAngle (m_y, yMinLimit, yMaxLimit);
				
			} else if (Input.GetMouseButton (1)) {
				
				m_Distance -= Input.GetAxis ("Mouse Y") * zoomMultiplier;
				
				if (m_Distance < minDist)
					m_Distance = minDist;
				if (m_Distance > maxDist)
					m_Distance = maxDist;
			}
		}
		
		// Mouse scroll wheel 
		if (Input.GetAxis ("Mouse ScrollWheel") != 0 && canInteract) {
			m_Distance -= Input.GetAxis ("Mouse ScrollWheel") * ySpeed * zoomMultiplier;
			
			if (m_Distance < minDist)
				m_Distance = minDist;
			if (m_Distance > maxDist)
				m_Distance = maxDist;
		}
		
		UpdateCamera ();
	}
	
	public void UpdateCamera ()
	{
		if (!followTarget)
			return;
		
		m_CameraTarget = followTarget.position;
		Vector3 dist = new Vector3 (0.0f, 0.0f, -m_Distance);
		Quaternion rotation = Quaternion.Euler (m_y, m_x, 0.0f);
		Vector3 position = rotation * dist + m_CameraTarget;
		
		transform.rotation = rotation;
		transform.position = position;
	}
	
	float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		
		return Mathf.Clamp (angle, min, max);
	}
	public void getTouchedPoint ()
	{
		/*
		// thin ray!
		RaycastHit hit;
		if (Physics.Raycast (camera.ScreenPointToRay (Input.mousePosition), out hit)) {
			if (hit.collider) {
				smoothFollow.target = hit.transform;
			}
		}
		*/
		
		// thick ray!
		Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] hits;
		// sรณ disparar o evento se o objeto mouse doubled clicked for um objeto com um collider
		//hits = Physics.CapsuleCastAll (screenRay.origin, screenRay.origin, 2, screenRay.direction);
		hits = Physics.SphereCastAll (screenRay.origin, 1, screenRay.direction);
		
		if (hits.Length > 0) {
			smoothFollow.target = hits [0].transform;
		}
	}
}
