using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class InputSUD : MonoBehaviour {
	Slider slider;
	float speed = 10;
	StreamWriter fh;
	
	public CanvasGroup canvasGroup;

	public string filenamePrefix = "Session_";
	public string filenameTimeFormat = "dd-MM-yyyy_hh-mm";
	public string filenameFiletype = ".csv";

	public string logTimeFormat = "hh:mm:ss";
	public string logFirstLine = "timestamp, SUD value";

	public bool hide;
	Vector3 oldPos;



	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		CreateSession ();
		Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if (hide) {
			return;
		}

		slider.value += Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
		if (Input.GetButtonDown ("Submit")) {
			UpdateSession (slider.value);
			Hide ();
			//slider.value = 5; // reset
		}
	}

	void CreateSession(){
		string filename = filenamePrefix + System.DateTime.Now.ToString (filenameTimeFormat)+filenameFiletype;
		fh = File.CreateText (filename);
		fh.WriteLine (logFirstLine);
		Debug.Log ("Session started: "+filename);
	}

	void UpdateSession(float value){
		fh.WriteLine (System.DateTime.Now.ToString(logTimeFormat)+","+value);
		Debug.Log ("Session update: "+ System.DateTime.Now.ToString("hh:mm")+","+value);
	}

	void CloseSession(){
		fh.Close ();

		Debug.Log ("Session closed");
	}

	void OnApplicationQuit(){
		CloseSession ();
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
