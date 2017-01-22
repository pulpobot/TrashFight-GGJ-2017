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

	public float[] mass;

	public GameObject smallSmoke;
	public GameObject largeSmoke;


	Vector3 startV;
	bool OnAir;
	Vector3 startingTransform;

	void Awake()
	{
		startingTransform = transform.localPosition;
		switch(objectSize)
		{
		case ObjectSize.Large:
			GetComponent<Rigidbody> ().mass = mass[2];
			break;
		case ObjectSize.Medium:
			GetComponent<Rigidbody> ().mass = mass[1];
			break;
		case ObjectSize.Small:
			GetComponent<Rigidbody> ().mass = mass[0];
			break;
		}

	}

	// Use this for initialization
	void Start () {
		startV = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void LaunchObject(Vector3 launchForce)
	{
		GetComponent<Rigidbody> ().isKinematic = false;
		GetComponent<Rigidbody> ().AddForce (launchForce, ForceMode.Impulse);
		GetComponent<Rigidbody> ().AddTorque (new Vector3(Random.Range(-150,150),Random.Range(-150,150),Random.Range(-150,150)));
		OnAir = true;
	}

	public void ResetPosition()
	{
		transform.localPosition = startingTransform;
	}

	void OnTriggerEnter(Collider obj)
	{
		if (OnAir) 
		{
			GardenPad aux = obj.GetComponent<GardenPad> ();
			if (aux != null) 
			{
				aux.OnHit (objectSize);
			}
			OnAir = false;

			if (objectSize == ObjectSize.Small)
				((GameObject)Instantiate (smallSmoke)).transform.position = transform.position;
			else
				((GameObject)Instantiate (largeSmoke)).transform.position = transform.position;

			GetComponent<AudioSource> ().Play ();

			transform.position = startV;
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().rotation = Quaternion.identity;
			GetComponent<Rigidbody> ().isKinematic = true;

			gameObject.SetActive (false);

		}
	}
}
