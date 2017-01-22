using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceMeterUIController : MonoBehaviour 
{
	public float minXValue = 516f;
	public float maxXValue = 516f;

	public Sprite[] objectsList;

	float currentValue;
	void Awake()
	{
		currentValue = 0.1f;
		SetValue (currentValue);
	}

	public void SetImage(int index)
	{
		GetComponent<Image> ().sprite = objectsList [index];
	}

	public void SetValue(float value)
	{
		currentValue = 1-value;
		GetComponent<RectTransform> ().anchoredPosition = new Vector2 (Mathf.Lerp(maxXValue,minXValue, currentValue), GetComponent<RectTransform> ().anchoredPosition.y);
	}
}
