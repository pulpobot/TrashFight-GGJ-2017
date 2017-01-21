using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenPad : MonoBehaviour 
{
	public GardenPad[] closestGardens;
	public GardenPad[] farthestGardens;

	public float health = 1;

	public Color[] damageColors;

	void Start()
	{
		OnWaveHit (0);
	}

	public void OnHit(ObjectSize objSize)
	{
		GetComponent<Renderer> ().enabled = true;

		int range = 0;

		float damage = 0;

		switch(objSize)
		{
		case ObjectSize.Large:
		range = 2;
		damage = 0.75f;
			break;
		case ObjectSize.Medium:
			range = 1;
		damage = 0.5f;
			break;
		case ObjectSize.Small:
			range = 0;
		damage = 0.25f;
			break;
		}

		OnWaveHit (damage);

		//Affect the closest ones
		if (range > 0) 
		{
			damage -= 0.25f;
			for (int i = 0; i < closestGardens.Length; i++) 
			{
				closestGardens [i].OnWaveHit (damage);
			}
		}

		//Affect the farthest ones
		if (range > 1) 
		{
			damage -= 0.25f;
			for (int i = 0; i < farthestGardens.Length; i++) 
			{
				farthestGardens [i].OnWaveHit (damage);
			}
		}
	}

	public void OnWaveHit(float hitDamage)
	{
		health -= hitDamage;

		GetComponent<Renderer> ().enabled = true;

		if (health <= 0.75f)
			GetComponent<Renderer> ().material.color = damageColors [0];
		if (health <= 0.5f) 
			GetComponent<Renderer> ().material.color = damageColors [1];
		if (health <= 0.25f) 
			GetComponent<Renderer> ().material.color = damageColors [2];
		if (health <= 0f) 
			GetComponent<Renderer> ().material.color = damageColors [3];
//		if (hitDamage == 0.75f)
//			GetComponent<Renderer> ().material.color = Color.red;
//		else if (hitDamage == 0.5f) 
//			GetComponent<Renderer> ().material.color = Color.blue;
//		else if (hitDamage == 0.25f) 
//			GetComponent<Renderer> ().material.color = Color.green;
	}
}

