using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenPad : MonoBehaviour 
{
	public GardenPad[] closeGardens;

	public void OnHit()
	{
		GetComponent<Renderer> ().enabled = true;

		for (int i = 0; i < closeGardens.Length; i++) 
		{
			GetComponent<Renderer> ().enabled = true;
			GetComponent<Renderer> ().material.color = Color.red;

		}
	}
}
