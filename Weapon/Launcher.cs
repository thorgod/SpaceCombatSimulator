using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {
	public GameObject prefeb;
	public int MissileSpeed = 50;
	public int MissileDamage =100;
	public float timeToLive = 5;
	public bool HeatSeaking = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Fire_It") || Input.GetKeyDown(KeyCode.LeftControl)) 
		{

			StartCoroutine(spawn ());

		}
	}
	IEnumerator spawn()
	{
		GameObject gameObject = Instantiate(prefeb,transform.position,transform.rotation) as GameObject;

		Missile_Simple missile_script = gameObject.GetComponent<Missile_Simple> ();
		missile_script.Speed = MissileSpeed ;
		missile_script.Damage = MissileDamage;
		missile_script.IsHeatSeaking = HeatSeaking;
		missile_script.timeToLive = timeToLive;
		missile_script.GetComponent<Rigidbody>().velocity = transform.parent.GetComponent<Rigidbody>().velocity;
		yield return new WaitForSeconds(1);
	}
}
