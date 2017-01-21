using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLaunch : MonoBehaviour {

	public Vector3 force;

	Vector3 startV;
	bool OnAir;
	// Use this for initialization
	void Start () {
		startV = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void LaunchObject()
	{
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
		OnAir = true;
	}

	void OnCollisionEnter(Collision obj)
	{
		if (OnAir) 
		{
			transform.position = startV;	
			OnAir = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().rotation = Quaternion.identity;
			GetComponent<Rigidbody> ().isKinematic = true;
		}
	}
}
