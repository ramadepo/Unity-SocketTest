using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningCreature : MonoBehaviour {

	public GameObject TheSpawn;
	public Transform[] SpawnPoints;
	public float SpawnTime=3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("s")) {
			Spawn ();
		}
	}

	public void Spawn(){	//spawn the gameobject in random place
		int SpawnPointNumber = Random.Range (0, SpawnPoints.Length);
		Instantiate (TheSpawn, SpawnPoints [SpawnPointNumber].position, SpawnPoints [SpawnPointNumber].rotation);
	}
}
