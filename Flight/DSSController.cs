//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class DSSController : MonoBehaviour
{
	#region Power Plant Related
	
	public Dictionary<int, PowerPlant> PowerPlants;
	
	public void RegisterPowerPlant(PowerPlant power)
	{
		if (PowerPlants == null)
			PowerPlants = new Dictionary<int, PowerPlant> (); 
		PowerPlants[power.ID] = power;
	}
	
	#endregion Power Plant Related

	public Dictionary<Vector3, Vector3> ForceVectors { get; private set; }
	private float originalDrag;                                         // The drag when the scene starts.
	private float originalAngularDrag;                                  // The angular drag when the scene starts.
	private float aeroFactor;
	
	bool immobilized = false;											// used for making the plane uncontrollable, i.e. if it has been hit or crashed.

	void Start ()
	{
		// Store original drag settings, these are modified during flight.
		originalDrag = GetComponent<Rigidbody>().drag;
		originalAngularDrag = GetComponent<Rigidbody>().angularDrag;
	}
	
	public void Move(Dictionary<Vector3, Vector3> forceVectors)
	{
		this.ForceVectors = forceVectors;

		CalculateVectorForces ();
	}

	void CalculateVectorForces ()
	{
		int i = 0;
		foreach (var kv in ForceVectors) 
		{
			var force = -kv.Value;
			GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Force);	
			var offset = kv.Key - GetComponent<Rigidbody>().centerOfMass;
			var torque = Vector3.Cross(offset, force);
			//var torque = Vector3.Cross(torque1, force) / Vector3.Dot(force, force);
			GetComponent<Rigidbody>().AddRelativeTorque(torque);
			int index = (i++)%2;
		}
	}
	
	// Immobilize can be called from other objects, for example if this plane is hit by a weapon and should become uncontrollable
	public void Immobilize ()
	{
		immobilized = true;
	}
	
	// Reset is called via the ObjectResetter script, if present.
	public void Reset()
	{
		immobilized = false;
	}
}