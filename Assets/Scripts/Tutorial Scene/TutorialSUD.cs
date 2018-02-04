using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class TutorialSUD : MonoBehaviour {
	Slider slider;
	float speed = 10;
	StreamWriter fh;
	
	public CanvasGroup canvasGroup;
	
	public bool hide;
	Vector3 oldPos;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if (hide) {
			return;
		}
		
		slider.value += Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		if (Input.GetButtonDown ("Submit")) {
			Hide ();
			//slider.value = 5; // reset
		}
	}
	
	public void Show(){
		hide = false;
		canvasGroup.alpha = 1;
		//transform.root.GetComponent<Canvas> ().enabled = true;
	}
	
	public void Hide(){
		hide = true;
		canvasGroup.alpha = 0;
		//transform.root.GetComponent<Canvas> ().enabled = false;
	}
}
