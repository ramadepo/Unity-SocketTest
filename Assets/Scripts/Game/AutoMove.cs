using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMove : MonoBehaviour {

	Transform Destination;
	NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		Destination = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		navMeshAgent.SetDestination (Destination.position);	//destination is player
	}
}
