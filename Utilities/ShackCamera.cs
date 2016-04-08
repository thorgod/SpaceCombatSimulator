using UnityEngine;
using System.Collections;

public class ShackCamera : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shake = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.3f;
	public float decreaseFactor = .2f;
	public float shake_intensityR = .3f;
	private float shackInt;
	Vector3 originalPos;
	Quaternion originalRot;
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
		originalRot = camTransform.localRotation;
	}
	
	void Update()
	{
		float thrust = Input.GetAxis ("Thrust");
		if (thrust > 0) {
			shake += Time.deltaTime * .01f;
			shackInt += Time.deltaTime * .01f;
						
		} 

		if (shake > 0) {
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shackInt;
				camTransform.localRotation = new Quaternion (
				originalRot.x + Random.Range (-shackInt, shackInt) * .2f,
				originalRot.y + Random.Range (-shackInt, shackInt) * .2f,
				originalRot.z + Random.Range (-shackInt, shackInt) * .2f,
				originalRot.w + Random.Range (-shackInt,shackInt) * .2f);

				
				shake -= Time.deltaTime * decreaseFactor;
		} else {
				shackInt = shake_intensityR;
				shake = 0f;
				camTransform.localPosition = originalPos;
				camTransform.localRotation = originalRot;
		}

	}
}