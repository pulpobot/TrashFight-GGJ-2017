using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectSize
{
	Large,
	Medium,
	Small
}

public class ObjectToLaunch : MonoBehaviour {

	public Vector3 force;
	public ObjectSize objectSize;


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

	void OnTriggerEnter(Collider obj)
	{
		if (OnAir) 
		{
			OnAir = false;
			obj.GetComponent<GardenPad> ().OnHit (objectSize);

			transform.position = startV;	
			OnAir = false;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().rotation = Quaternion.identity;
			GetComponent<Rigidbody> ().isKinematic = true;
			gameObject.SetActive (false);
		}
	}
}
