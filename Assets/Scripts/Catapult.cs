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
	public RoundStep currentStep;

	public GameObject[] objectsToThrow;
	public float objectChangesPerSecond = 1.5f;
	public float laneChangesPerSecond = 1.5f;

	public Transform[] lanePos;

	int currentIndexObject;
	int currentIndexLane;

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
			OnStartLaneSelection ();
				break;

		case RoundStep.Rotation:
			currentStep = RoundStep.Force;
			OnLaneSelected ();
			break;

		case RoundStep.Force:
			OnThrowObject ();
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
			yield return new WaitForSeconds (1/objectChangesPerSecond);
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
		objectsToThrow [currentIndexObject].GetComponent<ObjectToLaunch> ().LaunchObject ();
	}

	void OnStartLaneSelection()
	{
		StartCoroutine ("OnLaneSelection");
	}

	void OnLaneSelected()
	{
		StopCoroutine ("OnLaneSelection");
	}

	IEnumerator OnLaneSelection()
	{
		currentIndexLane = Random.Range(0, lanePos.Length);

		Vector3 newPos = lanePos [currentIndexLane].position;
		newPos.y = transform.position.y;
		newPos.z = transform.position.z;

		transform.position = newPos;
		while (true) 
		{
			yield return new WaitForSeconds (1/laneChangesPerSecond);
			currentIndexLane++;
			if (currentIndexLane >= lanePos.Length) 
			{
				currentIndexLane = 0;
			}

			newPos = lanePos [currentIndexLane].position;
			newPos.y = transform.position.y;
			newPos.z = transform.position.z;

			transform.position = newPos;
		}
	}

}
