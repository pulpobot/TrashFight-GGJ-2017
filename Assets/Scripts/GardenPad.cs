﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenPad : MonoBehaviour 
{
	public GardenPad[] closestGardens;
	public GardenPad[] farthestGardens;

	public float health = 1;

	public GameObject[] decals;
	public float maxVerticalAlignment;
	private float initialAlignment;

	void Start()
	{
		initialAlignment = transform.parent.position.y;
		OnWaveHit (0);
		decals [0].SetActive (false);
	}

	public void OnHit(ObjectSize objSize)
	{
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
		StartCoroutine (AnimateDecal ());

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

		//StartCoroutine (AnimateWave(false));
		//StartCoroutine (AnimateDecal ());
		transform.parent.GetComponent<Animator> ().enabled = true;
		if (hitDamage >= 0.75f)
			transform.parent.GetComponent<Animator> ().Play ("WaveLarge");
		else if (hitDamage >= 0.5f) 
			transform.parent.GetComponent<Animator> ().Play ("WaveMedium");
		else if (hitDamage >= 0.25f) 
			transform.parent.GetComponent<Animator> ().Play ("WaveSmall");

		StartCoroutine (AnimateWave());
	}

	IEnumerator AnimateWave()
	{
		yield return new WaitForSeconds(1);
		transform.parent.GetComponent<Animator> ().enabled = false;
	
		Vector3 alignment = new Vector3(transform.parent.position.x, Mathf.Lerp (initialAlignment, maxVerticalAlignment, 1-health), transform.parent.position.z);
		float t = 0;
		Vector3 aux = transform.parent.position;
		aux.y = initialAlignment;

		while (t <= 1)
		{
			transform.parent.position = Vector3.Lerp (aux, alignment, t);
			yield return null;
			t += Time.deltaTime*1f;
		}
//
//		if (animateDecal)
//			StartCoroutine (AnimateDecal ());
//		
//		while (t <= 1)
//		{
//			transform.parent.position = Vector3.Lerp (aux, alignment, Mathf.Sin (Mathf.Deg2Rad*360*t));
//			yield return null;
//			t += Time.deltaTime*1.5f;
//		}
//
//		aux = transform.parent.position;
//		t = 0;

	}

	IEnumerator AnimateDecal()
	{
		float t = 0;
		decals [0].SetActive (true);
		Color auxColor = decals [0].GetComponent<SpriteRenderer> ().color;
		Color targetColor = new Color (auxColor.r, auxColor.g, auxColor.b, 0);

		while (t <= 1) 
		{
			decals [0].GetComponent<SpriteRenderer> ().color = Color.Lerp (targetColor, auxColor, t);
			yield return null;
			t += Time.deltaTime*2f;
		}

		t = 0;
		auxColor = decals [0].GetComponent<SpriteRenderer> ().color;
		while (t <= 1) 
		{
			decals [0].GetComponent<SpriteRenderer> ().color = Color.Lerp (auxColor,targetColor, t);
			yield return null;
			t += Time.deltaTime*0.2f;
		}

		yield return new WaitForSeconds(1.5f);
		auxColor.a = 1;
		decals [0].SetActive (false);
		decals [0].GetComponent<SpriteRenderer> ().color = auxColor;
	}
}

