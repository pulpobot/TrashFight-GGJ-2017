using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenController : MonoBehaviour 
{
	public GardenPad[] pads;

	public float CalculateHealth()
	{
		float health = 0;

		for (int i = 0; i < pads.Length; i++) 
		{
			health += pads [i].health;
		}

		return health;
	}
}
