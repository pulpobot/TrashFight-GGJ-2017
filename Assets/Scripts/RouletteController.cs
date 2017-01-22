using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteController : MonoBehaviour {
	public Animator roulette;

	public Sprite[] objectsList;
	public Image mainImage;
	public Image[] decorators;

	public void StartRoulette()
	{
		roulette.Play ("Idle");

		mainImage.enabled = false;
		for (int i = 0; i < decorators.Length; i++) 
		{
			decorators [i].enabled = false;
		}

		StartCoroutine ("PlaySound");
	}

	IEnumerator PlaySound()
	{
		while (true) 
		{
			GetComponent<AudioSource> ().Play();
			yield return new WaitForSeconds (0.015f);
		}
	}

	public void StopRoulette(int value)
	{
		roulette.Play ("Stop");
		StopCoroutine ("PlaySound");

		mainImage.enabled = true;
		mainImage.sprite = objectsList [value];

		for (int i = 0; i < decorators.Length; i++) 
		{
			decorators [i].enabled = true;
		}
	}

}
