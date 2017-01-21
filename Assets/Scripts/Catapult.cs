using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundStep
{
	Pause,
	Select,
	Rotation,
	Force,
	Launch
}

public class Catapult : MonoBehaviour 
{

	public GameObject[] objectsToThrow;
	public float changesPerSecond = 1.5f;

	public RoundStep currentStep;


	int currentIndexObject;

	void Start()
	{
		currentStep = RoundStep.Pause;
	}

	public void OnTap()
	{
		switch(currentStep)
		{
		case RoundStep.Pause:
			currentStep = RoundStep.Select;
			OnStartSelection ();
				break;

		case RoundStep.Select:
			currentStep = RoundStep.Rotation;
			OnObjectSelected ();
				break;

		case RoundStep.Rotation:
			currentStep = RoundStep.Force;
			OnThrowObject ();
			break;

		case RoundStep.Force:
			break;

		case RoundStep.Launch:
			break;
		}
	}

	void OnStartSelection()
	{
		StartCoroutine ("OnSelectionChanger");
	}

	IEnumerator OnSelectionChanger()
	{
		currentIndexObject = Random.Range(0, objectsToThrow.Length);
		objectsToThrow [currentIndexObject].SetActive (true);
		while (true) 
		{
			yield return new WaitForSeconds (1/changesPerSecond);
			objectsToThrow [currentIndexObject].SetActive (false);
			currentIndexObject++;
			if (currentIndexObject >= objectsToThrow.Length) 
			{
				currentIndexObject = 0;
			}

			objectsToThrow [currentIndexObject].SetActive (true);
		}
	}

	void OnObjectSelected()
	{
		StopCoroutine ("OnSelectionChanger");
	}

	void OnThrowObject()
	{
		objectsToThrow [currentIndexObject].GetComponent<ObjectCatapult> ().LaunchObject ();
	}
}
