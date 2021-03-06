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

[RequireComponent(typeof(ParticleSystem))]
public class PowerPlant: MonoBehaviour
{
	[SerializeField] public int ID;
	[SerializeField] public float thrust;
	[SerializeField] public float dryThrust;
	[SerializeField] public float afterBurnThrust;
	[SerializeField] public string name;
	[SerializeField] public string message;

	public Color minColour;      

	private DSSController jet;                    // The jet that the particle effect is attached to
	private ParticleSystem system;                      // The particle system that is being controlled
	private float originalStartSize;                    // The original starting size of the particle system
	private float originalLifetime;                     // The original lifetime of the particle system
	private Color originalStartColor;                   // The original starting colout of the particle system

	// Use this for initialization
	void Start () {
		
		// get the aeroplane from the object hierarchy
		jet.RegisterPowerPlant (this);

		// get the particle system ( it will be on the object as we have a require component set up
		system = GetComponent<ParticleSystem>();
		
		// set the original properties from the particle system
		originalLifetime = system.startLifetime;
		originalStartSize = system.startSize;
		originalStartColor = system.startColor;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		// update the particle system based on the jets throttle
		system.startLifetime = Mathf.Lerp(0.0f, originalLifetime, thrust);
		system.startSize = Mathf.Lerp (originalStartSize*.3f, originalStartSize, thrust);
		system.startColor = Color.Lerp(minColour, originalStartColor, thrust);
	}

}

