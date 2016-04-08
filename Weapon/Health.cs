using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int health = 200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		Destroy (transform.parent.gameObject);
	}

	// Every script attached to the game object 
	// that has an ApplyDamage function will be called.
	void ApplyDamage (int damage) {
		Manager.Instance.astCount++;
		Damage (damage);
	}

	void Damage (int amt)
	{
		health -= amt;
		if(health <= 0)
		{
			Die ();
		}
	}
	void Die ()
	{
		if (transform.parent != null) {
						Destroy (transform.parent.gameObject);
		} 
		else 
		{
						Destroy (gameObject);
		}



	}
}
