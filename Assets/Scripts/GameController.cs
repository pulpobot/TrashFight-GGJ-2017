using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public float roundTime;
	public UITimerController timerController;

	public Catapult p1Catapult;
	public Button p1Button;
	public GardenController p1Garden;

	public Catapult p2Catapult;
	public Button p2Button;
	public GardenController p2Garden;

	public GameObject p1WinnerPanel;
	public GameObject p1LoserPanel;
	public GameObject p2WinnerPanel;
	public GameObject p2LoserPanel;

	public Text[] scoresP1;
	public Text[] scoresP2;

	void Start()
	{
		p1WinnerPanel.SetActive (false);
		p1LoserPanel.SetActive (false);
		p2WinnerPanel.SetActive (false);
		p2LoserPanel.SetActive (false);

		StartCoroutine (Timer());

	}

	IEnumerator Timer()
	{
		yield return new WaitForSeconds (1f);

		p1Button.interactable = true;
		p2Button.interactable = true;

		float t = roundTime;

		p1Catapult.OnTap ();
		p2Catapult.OnTap ();


		while (t > 0) 
		{
			timerController.Percentage = t / roundTime;
			yield return null;
			t -= Time.deltaTime;
		}

		p1Catapult.StopCatapults ();
		p1Button.interactable = false;
		p2Catapult.StopCatapults ();
		p2Button.interactable = false;

		float healthP1 = p1Garden.CalculateHealth ();
		float healthP2 = p2Garden.CalculateHealth ();

		for (int i = 0; i < scoresP1.Length; i++) 
		{
			scoresP1 [0].text = "Score: " + Mathf.RoundToInt(healthP2);
		}

		for (int i = 0; i < scoresP2.Length; i++) 
		{
			scoresP2 [0].text = "Score: " + Mathf.RoundToInt(healthP1);
		}

		if (healthP1 > healthP2) 
		{
			p1WinnerPanel.SetActive (true);
			p1LoserPanel.SetActive (false);
			p2WinnerPanel.SetActive (false);
			p2LoserPanel.SetActive (true);
		}
		else 
		{
			p1WinnerPanel.SetActive (false);
			p1LoserPanel.SetActive (true);
			p2WinnerPanel.SetActive (true);
			p2LoserPanel.SetActive (false);
		}

	}

}
