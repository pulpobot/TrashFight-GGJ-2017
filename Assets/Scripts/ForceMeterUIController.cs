using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceMeterUIController : MonoBehaviour 
{
	public float maxHeightYValue = 516f;
	public int divider = 8;

	int currentValue;
	void Awake()
	{
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (GetComponent<RectTransform> ().sizeDelta.x, 0);
		currentValue = 0;
	}

	public void SetValue(int value)
	{
		currentValue = value;
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (GetComponent<RectTransform> ().sizeDelta.x, (maxHeightYValue / divider) * currentValue);
	}
}
