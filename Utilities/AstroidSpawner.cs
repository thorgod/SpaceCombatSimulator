using UnityEngine;
using System.Collections;

public class AstroidSpawner : MonoBehaviour {
	public GameObject[] asteroids;
	private Vector3 origin = Vector3.zero;
	public bool keepGeneratingOnfly = false;
	public float distancefoward = 1000f;
	public float minSize = 0.2f;
	public float maxSize = 4.5f;
	
	public int minCount = 300;
	public int maxCount = 1500;
	
	public float minDistance = 500.0f;
	public float maxDistance = 5500.0f;
	public LayerMask mask = 11;
	// Use this for initialization
	void Start () {
		origin = transform.position;
		StartCoroutine(spawn ());
	}
	
	// Update is called once per frame
	void Update () {
		if (keepGeneratingOnfly) {
						Vector3 test = GetComponent<Rigidbody>().velocity;
						origin = transform.position + test.normalized * distancefoward;
						if (!Physics.CheckSphere (origin, (float)(minDistance / 2f), mask.value)) {
								StartCoroutine (spawn ());

						}
				}
	}

	IEnumerator spawn()
	{
		int count=(Random.Range(minCount, maxCount));

		for (int i = 0; i < count; i++) {
			float size = Random.Range(minSize, maxSize);
			GameObject prefab = asteroids[Random.Range(0, asteroids.Length)];
			Vector3 pos = new Vector3();
			bool found=false;
			for (int j = 0; j < 100; j++) 
			{
				pos = Random.insideUnitSphere * (minDistance + (maxDistance - minDistance) * Random.value);
				pos += origin;
				if (!Physics.CheckSphere(pos, (float)(size / 2.0))) {
					found = true;
					break;
					
				}
			}
			if(found)
			{
				GameObject go = (GameObject)Instantiate(prefab, pos, Random.rotation);
				go.transform.localScale.Set(size, size, size);
				if(Random.Range(0, 100)>96)
				{
					/*
					int children = go.transform.childCount;

					Vector3 torque= new Vector3();

					torque.x = Random.Range (-10, 10);
					torque.y = Random.Range (-10, 10);
					torque.z = Random.Range (-10, 10);

					go.transform.GetChild(0).rigidbody.AddForce(torque*1000);
					torque.x = Random.Range (-10, 10);
					torque.y = Random.Range (-10, 10);
					torque.z = Random.Range (-10, 10);
					
					go.transform.GetChild(0).rigidbody.AddTorque(torque*1000);
						*/
				}
			}
			
		}
		yield return new WaitForSeconds(0);
	}
}
