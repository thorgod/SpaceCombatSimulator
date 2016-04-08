using UnityEngine;
using System.Collections;

public class Missile_Simple :  Weapon{
	public bool IsHeatSeaking = true;

	public float timeToLive = 5;
	public GameObject ExplodeThis;

	public float flyTime = (float).01;

	private GameObject closest;



	private float homingSensitivity = (float).8;

	// Use this for initialization
	void Start () 
	{
		if (IsHeatSeaking) {
						closest = FindClosestEnemy ();

				}
		Destroy (gameObject, timeToLive);
		

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		if (IsHeatSeaking && closest != null) 
		{

			var relativePos = closest.transform.position - transform.position;
			var rotation = Quaternion.LookRotation (relativePos);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, homingSensitivity);
			transform.Translate (0, 0, Speed * Time.deltaTime, Space.Self);
			//rigidbody.AddForce(forward*Speed* Time.deltaTime);

		}
		else
		{
			GetComponent<Rigidbody>().AddForce(transform.forward*Speed*2);
			//transform.Translate (0, 0, Speed * Time.deltaTime, Space.Self);
		}


	}

	void OnTriggerEnter(Collider other)
	{

		Instantiate(ExplodeThis, transform.position, transform.rotation);
		other.SendMessage ("ApplyDamage", Damage);
		Destroy (gameObject, 0.0f);
	}

	GameObject FindClosestEnemy() {
				GameObject[] gos;
				gos = GameObject.FindGameObjectsWithTag ("THOR");
				GameObject closest = null;
				float distance = Mathf.Infinity;
				Vector3 position = transform.position;
				foreach (GameObject go in gos) {
						Vector3 diff = go.transform.position - position;
						float curDistance = diff.sqrMagnitude;

						Vector3 targetDir =  go.transform.position-transform.position;
						Vector3 forward = transform.forward;
							
						float angle = Vector3.Angle (targetDir, forward);
						if (curDistance < distance && angle < 5f) {
								closest = go;
								distance = curDistance;
						}
				}
				return closest;
		}
}
