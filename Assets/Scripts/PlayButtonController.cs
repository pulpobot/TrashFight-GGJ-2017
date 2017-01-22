using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour {

	public Color tintColor;
	public Renderer playButtonRend;

	void OnMouseDown() {
		SceneManager.LoadScene("GameplayDev_Test");
	}

	void OnMouseOver() {
		GetComponent<Renderer>().material.color = tintColor;
		playButtonRend.material.color = tintColor;
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = Color.white;
		playButtonRend.material.color = Color.white;

	}
}
