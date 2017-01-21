using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimerController : MonoBehaviour 
{
	float percentage;
	public float Percentage
	{
		set
		{
			percentage = value;
			GetComponent<Image> ().fillAmount = percentage;
		}

		get
		{
			return percentage;
		}
	}

}
