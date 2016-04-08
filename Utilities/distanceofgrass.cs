using UnityEngine;
using System.Collections;

public class distanceofgrass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Terrain.activeTerrain.detailObjectDistance = 5000;
	}
}
