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
	public float forceChangesPerSecond = 1.5f;

	public Transform[] lanePos;

	public int forcesQuantity = 8;
	public Vector3[] forces;
	public Vector3 minForce;
	public Vector3 maxForce;

	public ForceMeterUIController forceMeter;

	public RouletteController roulette;
	public Animator arrow;

	int currentIndexObject;
	int currentIndexLane;
	float currentForce;

	Vector3 startingPos;

	void Awake()
	{
		startingPos = transform.position;
	}

	void Start()
	{
		forceMeter.transform.parent.GetComponent<Animator> ().Play ("RouletteHide");
		arrow.Play ("RouletteHide");
		currentStep = RoundStep.Pause;
	}

	public void StopCatapults()
	{
		currentStep = RoundStep.Pause;
		StopAllCoroutines ();
		roulette.StopRoulette (0);
	}

	public void OnTap()
	{
		switch(currentStep)
		{
		case RoundStep.Pause:
			currentStep = RoundStep.Select;
			roulette.StartRoulette ();
			OnStartSelection ();
				break;

		case RoundStep.Select:
			currentStep = RoundStep.Rotation;
			roulette.StopRoulette (currentIndexObject);
			OnObjectSelected ();
			OnStartLaneSelection ();
				break;

		case RoundStep.Rotation:
			currentStep = RoundStep.Force;
			OnLaneSelected ();
			OnStartForceSelection ();
			break;

		case RoundStep.Force:
			currentStep = RoundStep.Launch;
			OnForceSelected ();
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
		while (true) 
		{
			yield return new WaitForSeconds (1/objectChangesPerSecond);
			currentIndexObject++;
			if (currentIndexObject >= objectsToThrow.Length) 
			{
				currentIndexObject = 0;
			}

		}
	}

	void OnObjectSelected()
	{
		StopCoroutine ("OnSelectionChanger");
		forceMeter.SetImage (currentIndexObject);
	}

	void OnThrowObject()
	{
		GetComponent<AudioSource> ().Play ();
		objectsToThrow [currentIndexObject].SetActive (true);
		objectsToThrow [currentIndexObject].GetComponent<ObjectToLaunch> ().LaunchObject (Vector3.Lerp(minForce, maxForce, currentForce));
		StartCoroutine (WaitToResetTurn());
	}

	IEnumerator WaitToResetTurn()
	{
		yield return new WaitForSeconds (3f);
		currentStep = RoundStep.Pause;
		ResetPosition ();
		OnTap ();
	}

	void ResetPosition()
	{
		transform.position = startingPos;
		for (int i = 0; i < objectsToThrow.Length; i++) 
		{		
			objectsToThrow [i].GetComponent<ObjectToLaunch> ().ResetPosition ();
		}
	}

	void OnStartLaneSelection()
	{
		StartCoroutine ("OnLaneSelection");
	}

	void OnLaneSelected()
	{
		StopCoroutine ("OnLaneSelection");
		arrow.Play ("RouletteHide");
	}

	IEnumerator OnLaneSelection()
	{
		currentIndexLane = Random.Range(0, lanePos.Length);
		arrow.Play ("RouletteShow");
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

	void OnStartForceSelection()
	{
		StartCoroutine ("OnForceSelection");
	}

//	IEnumerator OnForceSelection()
//	{
//		currentIndexForce = Random.Range(0, forces.Length);
//		while (true) 
//		{
//			yield return new WaitForSeconds (1/forceChangesPerSecond);
//			currentIndexForce++;
//			forceMeter.SetValue (currentIndexForce);
//
//			if (currentIndexForce >= forces.Length) 
//			{
//				currentIndexForce = 0;
//			}
//
////			yield return new WaitForSeconds (1/forceChangesPerSecond);
////			currentIndexForce = 7;
////			forceMeter.SetValue (currentIndexForce);
//		}
//	}


	IEnumerator OnForceSelection()
	{
		forceMeter.transform.parent.GetComponent<Animator> ().Play ("RouletteShow");
		currentForce = 0;
		int multiplier = 1;
		while (true) 
		{
			yield return null;
			currentForce += multiplier * 0.08f;

			if (currentForce >= 1 || currentForce <= 0.1f) 
			{
				multiplier *= -1;
			}
			currentForce = Mathf.Clamp (currentForce, 0.1f, 1f);

			forceMeter.SetValue (currentForce);
			//			yield return new WaitForSeconds (1/forceChangesPerSecond);
			//			currentIndexForce = 7;
			//			forceMeter.SetValue (currentIndexForce);
		}
	}

	void OnForceSelected()
	{
		StopCoroutine ("OnForceSelection");
		forceMeter.transform.parent.GetComponent<Animator> ().Play ("RouletteHide");

	}

}
