using UnityEngine;
using System.Collections;

public class Weapon: MonoBehaviour 
{
	public int Health =0;
	public int Speed = 100;
	public int Damage = 50;



	public enum WeaponType
	{
		Missile,
		Bullet,
		Laser
	}

	void OnTriggerEnter(Collider other)
	{
		other.SendMessage ("ApplyDamage", Damage);
	}

	public Weapon ()
	{
	}
}


